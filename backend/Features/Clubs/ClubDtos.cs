using System;

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
    IReadOnlyList<BayDto> Bays);

public record BayDto(
    int Id,
    string Name,
    bool IsActive);



