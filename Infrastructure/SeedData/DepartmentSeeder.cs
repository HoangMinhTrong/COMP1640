using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public class DepartmentSeeder
    {
        public static void Seeds(ModelBuilder builder, Tenant tenant)
        {
            var deparments = new List<Department>()
            {
                new Department
                {
                    Id = 1,
                    Name = "Computing",
                    TenantId = tenant.Id,
                },
                new Department
                {
                    Id = 2,
                    Name = "Business",
                    TenantId = tenant.Id,
                },
                new Department
                {
                    Id = 3,
                    Name = "Design",
                    TenantId = tenant.Id,
                }
            };

            builder.Entity<Department>().HasData(deparments);
        }
    }
}
