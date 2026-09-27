using Microsoft.EntityFrameworkCore;
using GoGolf.Api.Data;
using GoGolf.Api.Features.Booking;

namespace GoGolf.Api.Features.Clubs;

public static class ClubEndpoints
{
    public static RouteGroupBuilder MapClubEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clubs");

        group.MapGet("/", async (AppDbContext dbContext) =>
            await dbContext.Clubs
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new ClubSummaryDto(c.Id, c.Name, c.Address, c.PlayerRates.Min(r => r.Price), c.Rating, c.Bays.Count(b => b.IsActive)))
                .ToListAsync());
        
        group.MapGet("/{id:int}", async (int id, AppDbContext dbContext, TimeProvider clock) =>
        {
            var club = await dbContext.Clubs
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new 
                {
                    c.Id, c.Name, c.Address, c.Rating, c.TimeZoneId,
                    FromPrice = c.PlayerRates.Min(r => r.Price),
                    OperatingHours = c.OperatingHours
                        .OrderBy(h => h.DayOfWeek)
                        .Select(h => new OperatingHoursDto(h.DayOfWeek, h.OpensAt, h.ClosesAt))
                        .ToList(),
                    Bays = c.Bays
                        .Where(b => b.IsActive)
                        .OrderBy(b => b.Name)
                        .Select(b => new BayDto(b.Id, b.Name, b.Description))
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
            var clubToday = DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(clock.GetUtcNow(), timeZone).DateTime);
            var bookableUntil = clubToday.AddDays(BookingWindow.MaxDaysInAdvance);

            return Results.Ok(new ClubDetailDto(
                club.Id, club.Name, club.Address, club.FromPrice, club.Rating,
                clubToday, bookableUntil, club.OperatingHours, club.Bays, club.PlayerRates));
        });

        return group;
    }
}
