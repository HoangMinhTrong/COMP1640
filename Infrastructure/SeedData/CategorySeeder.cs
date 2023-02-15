using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public class CategorySeeder
    {
        public static void Seeds(ModelBuilder builder, Tenant tenant)
        {
            var categories = new List<Category>() { 
                new Category
                {
                    Id = 1,
                    Name = "Category 1",
                    TenantId = tenant.Id,
                },
                new Category
                {
                    Id = 2,
                    Name = "Category 2",
                    TenantId = tenant.Id,
                }
            };

            builder.Entity<Category>().HasData(categories);
        }
    }
}
