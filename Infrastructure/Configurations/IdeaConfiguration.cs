using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class IdeaConfiguration : IEntityTypeConfiguration<Idea>
{
    public void Configure(EntityTypeBuilder<Idea> builder)
    {
        builder.HasOne(i => i.CreatedByNavigation)
            .WithMany(u => u.Ideas)
            .HasForeignKey(i => i.CreatedBy)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(i => i.AcademicYear)
            .WithMany(u => u.Ideas)
            .HasForeignKey(i => i.AcademicYearId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}