using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class IdentitySeeder
    {
        public static void Seeds(ModelBuilder builder)
        {
            SeedRoles(builder);
            SeedUsers(builder);
            SeedUserRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .HasData(
                    new Role()
                    {
                        Id = (int)RoleTypeEnum.Admin,
                        Name = RoleTypeEnum.Admin.ToString(),
                    }
                 );
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();

            // Admin 
            var admin = new User
            {
                Id = 1,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            builder.Entity<User>().HasData(
                admin
            );
        }

        private static void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<int>>().HasData(
               new IdentityUserRole<int>
               {
                   // Admin
                   RoleId = (int)RoleTypeEnum.Admin,
                   UserId = 1
               });
        }

    }
}
