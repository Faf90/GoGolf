using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoGolf.Api.Models;

namespace GoGolf.Api.Data.Configurations;

public class PlayerRateConfiguration : IEntityTypeConfiguration<PlayerRate>
{
    public void Configure(EntityTypeBuilder<PlayerRate> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Price).HasPrecision(10, 2);

        builder.HasOne(r => r.Club)
            .WithMany(c => c.PlayerRates)
            .HasForeignKey(r => r.ClubId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => new { r.ClubId, r.PlayerCount })
            .IsUnique();

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_PlayerRate_PlayerCountPositive", "\"PlayerCount\" >= 1"));

        builder.HasData(SeedData());
    }

    private static IEnumerable<PlayerRate> SeedData()
    {
        var id = 1;
        var rows = new List<PlayerRate>();
        var prices = new[] { 150m, 200m, 250m, 300m };

        foreach (var clubId in new[] { 1, 2, 3 })
            for (var players = 1; players <= 4; players++)
                rows.Add(new PlayerRate
                {
                    Id = id++, ClubId = clubId,
                    PlayerCount = players, Price = prices[players - 1]
                });

        return rows;
    }
}
