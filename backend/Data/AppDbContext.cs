using Microsoft.EntityFrameworkCore;
using GoGolf.Api.Models;

namespace GoGolf.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Club> Clubs => Set<Club>();
    public DbSet<Bay> Bays => Set<Bay>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}