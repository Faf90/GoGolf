using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoGolf.Api.Models;

namespace GoGolf.Api.Data.Configurations;

public class OperatingHoursConfiguration : IEntityTypeConfiguration<OperatingHours>
{
    public void Configure(EntityTypeBuilder<OperatingHours> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasOne(o => o.Club)
            .WithMany(c => c.OperatingHours)
            .HasForeignKey(o => o.ClubId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(o => new { o.ClubId, o.DayOfWeek })
            .IsUnique();

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_TradingDay_ClosesAfterOpens", "\"ClosesAt\" > \"OpensAt\""));

        builder.HasData(SeedData());
    }

    private static IEnumerable<OperatingHours> SeedData()
    {
        var id = 1;
        var rows = new List<OperatingHours>();

        // Shank: open every day, 08:00–22:00
        foreach (var day in Enum.GetValues<DayOfWeek>())
            rows.Add(new OperatingHours { Id = id++, ClubId = 1, DayOfWeek = day,
                OpensAt = new TimeOnly(8, 0), ClosesAt = new TimeOnly(22, 0) });

        // Simpact: closed Mondays, 07:00–21:00
        foreach (var day in Enum.GetValues<DayOfWeek>().Where(d => d != DayOfWeek.Monday))
            rows.Add(new OperatingHours { Id = id++, ClubId = 2, DayOfWeek = day,
                OpensAt = new TimeOnly(7, 0), ClosesAt = new TimeOnly(21, 0) });

        // Golf Bar: weekends only, 09:00–22:00
        foreach (var day in new[] { DayOfWeek.Saturday, DayOfWeek.Sunday })
            rows.Add(new OperatingHours { Id = id++, ClubId = 3, DayOfWeek = day,
                OpensAt = new TimeOnly(9, 0), ClosesAt = new TimeOnly(22, 0) });

        return rows;
    }
}
