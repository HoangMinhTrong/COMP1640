using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Helpers;

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
                    Name = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.Admin),
                    NormalizedName = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.Admin).ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.QAManager,
                    Name = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.QAManager),
                    NormalizedName = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.QAManager).ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.DepartmentQA,
                    Name = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.DepartmentQA),
                    NormalizedName = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.DepartmentQA).ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.Staff,
                    Name = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.Staff),
                    NormalizedName = EnumMemberAttributeHelper.GetEnumMemberValue(RoleTypeEnum.Staff).ToUpper()
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

            // QA Manager 
            var director = new User
            {
                Id = 2,
                UserName = "qamanager@gmail.com",
                Email = "qamanager@gmail.com",
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "qamanager@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Department QA Coordinator 
            var computingDepartmentQA = new User
            {
                Id = 3,
                UserName = "computingdepartmentqa@gmail.com",
                Email = "computingdepartmentqa@gmail.com",
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "computingdepartmentqa@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            
            // Department QA Coordinator 
            var businessDepartmentQA = new User
            {
                Id = 4,
                UserName = "businessDepartmentQA@gmail.com",
                Email = "businessDepartmentQA@gmail.com",
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "businessDepartmentQA@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            
            // Department QA Coordinator 
            var designDepartmentQA = new User
            {
                Id = 5,
                UserName = "designDepartmentQA@gmail.com",
                Email = "designDepartmentQA@gmail.com",
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "designDepartmentQA@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Staff 
            var staff = new User
            {
                Id = 6,
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
                computingDepartmentQA,
                businessDepartmentQA,
                designDepartmentQA,
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
                    // QA Manager
                    UserId = 2,
                    RoleId = (int)RoleTypeEnum.QAManager,
                },
                new RoleUser
                {
                    // Computing QA Coordinator
                    UserId = 3,
                    RoleId = (int)RoleTypeEnum.DepartmentQA,
                },
                new RoleUser
                {
                    // Business QA Coordinator
                    UserId = 4,
                    RoleId = (int)RoleTypeEnum.DepartmentQA,
                },
                new RoleUser
                {
                    // Design QA Coordinator
                    UserId = 5,
                    RoleId = (int)RoleTypeEnum.DepartmentQA,
                },
                new RoleUser
                {
                    // Staff
                    UserId = 6,
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
                    // QA Manager
                    UserId = 2,
                    TenantId = Tenant.Id,
                },
                new TenantUser
                {
                    // Department QA
                    UserId = 3,
                    TenantId = Tenant.Id,
                }
                ,new TenantUser
                {
                    // Department QA
                    UserId = 4,
                    TenantId = Tenant.Id,
                },new TenantUser
                {
                    // Department QA
                    UserId = 5,
                    TenantId = Tenant.Id
                },new TenantUser
                {
                    // Staff
                    UserId = 6,
                    TenantId = Tenant.Id
                },
            };

            builder.Entity<TenantUser>().HasData(tenantUsers);
        }

    }
}
