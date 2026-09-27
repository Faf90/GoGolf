using GoGolf.Api.Features.Clubs;

namespace GoGolf.Api.Features.AvailableSlots;

public record AvailableSlotsDto(int ClubId, DateOnly Date, bool IsOpen,
    int SlotDurationMinutes, IReadOnlyList<PlayerRateDto> PlayerRates, IReadOnlyList<SlotDto> Slots);

public record SlotDto(DateTimeOffset StartsAt, DateTimeOffset EndsAt, 
    TimeOnly LocalTime, IReadOnlyList<AvailableBayDto> Bays);

public record AvailableBayDto(int Id, string Name, string? Description);
