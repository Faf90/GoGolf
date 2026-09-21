using System;

namespace GoGolf.Api.Models;

public class Bay
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Club Club { get; set; } = null!;
}
