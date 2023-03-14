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
                    OpenDate = new DateTime(2022, 2, 1),
                    ClosureDate = new DateTime(2022, 12, 30),
                    FinalClosureDate = new DateTime(2023, 1, 20),
                    TenantId = tenant.Id
                },
                
                new AcademicYear
                {
                    Id = 2,
                    Name = "2023 - 2024",
                    OpenDate = new DateTime(2023, 2, 1),
                    ClosureDate = new DateTime(2023, 12, 30),
                    FinalClosureDate = new DateTime(2024, 1, 20),
                    TenantId = tenant.Id
                },
            };

            builder.Entity<AcademicYear>().HasData(deparments);
        }
    }
}
