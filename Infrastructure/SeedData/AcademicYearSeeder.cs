using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public class AcademicYearSeeder
    {
        public static void Seeds(ModelBuilder builder, Tenant tenant)
        {
            var deparments = new List<AcademicYear>()
            {
                new AcademicYear
                {
                    Id = 1,
                    Name = "2022 - 2023",
                    ClosureDate = new DateTime(2022, 10, 30).ToUniversalTime(),
                    FinalClosureDate = new DateTime(2023, 3, 30).ToUniversalTime(),
                    EndDate = new DateTime(2023, 5, 1).ToUniversalTime(),
                    TenantId = 1
                },
                
                new AcademicYear
                {
                    Id = 2,
                    Name = "2023 - 2024",
                    ClosureDate = new DateTime(2023, 11, 30).ToUniversalTime(),
                    FinalClosureDate = new DateTime(2024, 4, 25).ToUniversalTime(),
                    EndDate = new DateTime(2023, 6, 1).ToUniversalTime(),
                    TenantId = 1
                },
            };

            builder.Entity<AcademicYear>().HasData(deparments);
        }
    }
}
