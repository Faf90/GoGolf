using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoGolf.Api.Models;

namespace GoGolf.Api.Data.Configurations;

public class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.FromPrice)
            .HasPrecision(10, 2);

        builder.Property(c => c.Rating)
            .HasPrecision(2, 1);

        builder.HasData(
            new Club { Id = 1, Name = "Shank", Address = "Moregloed, Pretoria", FromPrice = 150m, Rating = 5m },
            new Club { Id = 2, Name = "Simpact", Address = "Montana, Pretoria", FromPrice = 200m, Rating = 5m },
            new Club { Id = 3, Name = "Golf Bar", Address = "Fourways, Johannesburg", FromPrice = 185m, Rating = 4.7m }
        );
    }
}
