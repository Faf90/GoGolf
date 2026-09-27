using System;

namespace GoGolf.Api.Features.AvailableSlots;

public record SlotWindow(DateTimeOffset StartsAt, DateTimeOffset EndsAt, TimeOnly LocalTime);

public class SlotGenerator
{
    public static IReadOnlyList<SlotWindow> GenerateSlots(
        DateOnly date, TimeOnly opensAt, TimeOnly closesAt, int slotDurationMinutes, TimeZoneInfo timeZone)
    {
        var slots = new List<SlotWindow>();
        var slotDuration = TimeSpan.FromMinutes(slotDurationMinutes);
        
        var localStart = date.ToDateTime(opensAt);
        var localClose = date.ToDateTime(closesAt);
        var current = localStart;

        while (current + slotDuration <= localClose)
        {
            var startUtc = TimeZoneInfo.ConvertTimeToUtc(current, timeZone);
            slots.Add(new SlotWindow(
                new DateTimeOffset(startUtc),
                new DateTimeOffset(startUtc + slotDuration),
                TimeOnly.FromDateTime(current)));
            current += slotDuration;
        }

        return slots;
    }
}
