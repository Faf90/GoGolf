using System;

namespace GoGolf.Api.Models;

public class PlayerRate
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public int PlayerCount { get; set; }
    public decimal Price { get; set; }

    public Club Club { get; set; } = null!;
}
