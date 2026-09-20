using Microsoft.EntityFrameworkCore;
using GoGolf.Api.Models;

namespace GoGolf.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Club> Clubs => Set<Club>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>().HasData(
            new Club { Id = 1, Name = "Shank", Address = "Moregloed, Pretoria", FromPrice = 150m, Rating = 5m },
            new Club { Id = 2, Name = "Simpact", Address = "Montana, Pretoria", FromPrice = 200m, Rating = 5m },
            new Club { Id = 3, Name = "Golf Bar", Address = "Fourways, Johannesburg", FromPrice = 185m, Rating = 4.7m }
        );
    }
}