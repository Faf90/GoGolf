using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoGolf.Api.Models;

namespace GoGolf.Api.Data.Configurations;

public class BayConfiguration : IEntityTypeConfiguration<Bay>
{
    public void Configure(EntityTypeBuilder<Bay> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(b => b.IsActive)
            .IsRequired();
        
        builder.HasOne(b => b.Club)
            .WithMany(c => c.Bays)
            .HasForeignKey(b => b.ClubId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(b => new { b.ClubId, b.Name })
            .IsUnique();

        builder.HasData(
            // Shank — two bays
            new Bay { Id = 1, ClubId = 1, Name = "Bay 1", IsActive = true },
            new Bay { Id = 2, ClubId = 1, Name = "Bay 2", IsActive = true },
            new Bay { Id = 3, ClubId = 1, Name = "Bay 3", IsActive = true },

            // Simpact — five, one retired
            new Bay { Id = 4, ClubId = 2, Name = "Bay 1", IsActive = true },
            new Bay { Id = 5, ClubId = 2, Name = "Bay 2", IsActive = true },

            // Golf Bar — one bay
            new Bay { Id = 6, ClubId = 3, Name = "Bay 1", IsActive = true },
            new Bay { Id = 7, ClubId = 3, Name = "Bay 2", IsActive = true },
            new Bay { Id = 8, ClubId = 3, Name = "Bay 3", IsActive = true },
            new Bay { Id = 9, ClubId = 3, Name = "Bay 4", IsActive = true },
            new Bay { Id = 10, ClubId = 3, Name = "Bay 5", IsActive = true }
        );
    }
}
