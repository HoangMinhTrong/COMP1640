using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> builder)
        {
            builder.Property(x => x.OpenDate)
                   .HasColumnType("date");

            builder.Property(x => x.ClosureDate)
                   .HasColumnType("date");

            builder.Property(x => x.FinalClosureDate)
                   .HasColumnType("date");
        }
    }
}
