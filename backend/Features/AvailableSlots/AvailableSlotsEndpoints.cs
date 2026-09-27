using System;
using GoGolf.Api.Data;
using Microsoft.EntityFrameworkCore;
using GoGolf.Api.Features.Clubs;
using GoGolf.Api.Features.Booking;

namespace GoGolf.Api.Features.AvailableSlots;

public static class AvailableSlotsEndpoints
{
    public static IEndpointRouteBuilder MapAvailableSlotsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/clubs/{id:int}/available-slots", GetAvailableSlots);
            return app;
    }

    private static async Task<IResult> GetAvailableSlots(int id, DateOnly date, AppDbContext dbContext, TimeProvider clock)
    {
        var dayOfWeek = date.DayOfWeek;

        var club = await dbContext.Clubs
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.TimeZoneId,
                c.SlotDurationInMinutes,
                Hours = c.OperatingHours
                    .Where(h => h.DayOfWeek == dayOfWeek)
                    .Select(h => new { h.OpensAt, h.ClosesAt })
                    .FirstOrDefault(),
                Bays = c.Bays
                    .Where(b => b.IsActive)
                    .OrderBy(b => b.Name)
                    .Select(b => new AvailableBayDto(b.Id, b.Name, b.Description))
                    .ToList(),
                PlayerRates = c.PlayerRates
                    .OrderBy(r => r.PlayerCount)
                    .Select(r => new PlayerRateDto(r.PlayerCount, r.Price))
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (club is null)
            return Results.NotFound();
        
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(club.TimeZoneId);
        var now = clock.GetUtcNow();
        var clubToday = DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(now, timeZone).DateTime);
        var lastBookable = clubToday.AddDays(BookingWindow.MaxDaysInAdvance);

        if (date < clubToday || date > lastBookable)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["date"] = [$"Date must be between {clubToday:yyyy-MM-dd} and {lastBookable:yyyy-MM-dd}."]
            });
        }

        if (club.Hours is null)
        {
            return Results.Ok(new AvailableSlotsDto(
                ClubId: id,
                Date: date,
                IsOpen: false,
                SlotDurationMinutes: club.SlotDurationInMinutes,
                PlayerRates: club.PlayerRates,
                Slots: []));
        }

        var slots = SlotGenerator
            .GenerateSlots(
                date: date,
                opensAt: club.Hours.OpensAt,
                closesAt: club.Hours.ClosesAt,
                slotDurationMinutes: club.SlotDurationInMinutes,
                timeZone: timeZone)
            .Where(s => s.StartsAt > now)
            .Select(s => new SlotDto(
                StartsAt: s.StartsAt,
                EndsAt: s.EndsAt,
                LocalTime: s.LocalTime,
                Bays: club.Bays))
            .ToList();
        
        return Results.Ok(new AvailableSlotsDto(
            ClubId: id,
            Date: date,
            IsOpen: true,
            SlotDurationMinutes: club.SlotDurationInMinutes,
            PlayerRates: club.PlayerRates,
            Slots: slots));
    }   
}