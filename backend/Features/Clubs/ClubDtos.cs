using System;
using GoGolf.Api.Features.AvailableSlots;

namespace GoGolf.Api.Features.Clubs;

public record ClubSummaryDto(
    int Id, 
    string Name, 
    string Address, 
    decimal FromPrice, 
    decimal Rating,
    int BayCount);

public record ClubDetailDto(
    int Id, 
    string Name, 
    string Address, 
    decimal FromPrice, 
    decimal Rating,
    DateOnly BookableFrom,
    DateOnly BookableUntil,
    IReadOnlyList<OperatingHoursDto> OperatingHours,
    IReadOnlyList<BayDto> Bays,
    IReadOnlyList<PlayerRateDto> PlayerRates);

public record OperatingHoursDto(
    DayOfWeek DayOfWeek,
    TimeOnly OpensAt,
    TimeOnly ClosesAt);

public record BayDto(
    int Id,
    string Name,
    string? Description);

public record PlayerRateDto(int PlayerCount, decimal Price);



