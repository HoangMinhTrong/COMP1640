using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class IdentitySeeder
    {
        public static Tenant Tenant = new Tenant()
        {
            Id = 1,
            Name = "Greenwich Danang",
        };

        public static void Seeds(ModelBuilder builder)
        {
            SeedTenant(builder);
            SeedRoles(builder);
            SeedUsers(builder);
            SeedUserRoles(builder);
            SeedTenantUser(builder);
        }

        private static void SeedTenant(ModelBuilder builder)
        {
            builder.Entity<Tenant>().HasData(Tenant);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = (int)RoleTypeEnum.Admin,
                    Name = RoleTypeEnum.Admin.ToString(),
                    NormalizedName = RoleTypeEnum.Admin.ToString().ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.Director,
                    Name = RoleTypeEnum.Director.ToString(),
                    NormalizedName = RoleTypeEnum.Director.ToString().ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.Manager,
                    Name = RoleTypeEnum.Manager.ToString(),
                    NormalizedName = RoleTypeEnum.Manager.ToString().ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.Staff,
                    Name = RoleTypeEnum.Staff.ToString(),
                    NormalizedName = RoleTypeEnum.Staff.ToString().ToUpper()
                }
            };

            builder.Entity<Role>().HasData(roles);
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
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Director 
            var director = new User
            {
                Id = 2,
                UserName = "director@gmail.com",
                Email = "director@gmail.com",
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "director@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Manager 
            var manager = new User
            {
                Id = 3,
                UserName = "manager@gmail.com",
                Email = "manager@gmail.com",
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "manager@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Staff 
            var staff = new User
            {
                Id = 4,
                UserName = "staff@gmail.com",
                Email = "staff@gmail.com",
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "staff@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            builder.Entity<User>().HasData(
                admin,
                director,
                manager,
                staff
            );
        }

        private static void SeedUserRoles(ModelBuilder builder)
        {
            var userRoles = new List<RoleUser>()
            {
                new RoleUser
                {
                    // Admin
                    UserId = 1,
                    RoleId = (int)RoleTypeEnum.Admin,
                },
                new RoleUser
                {
                    // Director
                    UserId = 2,
                    RoleId = (int)RoleTypeEnum.Director,
                },
                new RoleUser
                {
                    // Manager
                    UserId = 3,
                    RoleId = (int)RoleTypeEnum.Manager,
                },
                new RoleUser
                {
                    // Staff
                    UserId = 4,
                    RoleId = (int)RoleTypeEnum.Staff,
                },
            };

            builder.Entity<RoleUser>().HasData(userRoles);
        }
        private static void SeedTenantUser(ModelBuilder builder)
        {
            var tenantUsers = new List<TenantUser>()
            {
                new TenantUser
                {
                    // Admin
                    UserId = 1,
                    TenantId = Tenant.Id,
                },
                new TenantUser
                {
                    // Director
                    UserId = 2,
                    TenantId = Tenant.Id,
                },
                new TenantUser
                {
                    // Manager
                    UserId = 3,
                    TenantId = Tenant.Id,
                }
                ,new TenantUser
                {
                    // Staff
                    UserId = 4,
                    TenantId = Tenant.Id,
                },
            };

            builder.Entity<TenantUser>().HasData(tenantUsers);
        }

    }
}
