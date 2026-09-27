using System;

namespace GoGolf.Api.Models;

public class OperatingHours
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly OpensAt { get; set; }
    public TimeOnly ClosesAt { get; set; }
    public Club Club { get; set; } = null!;
}
