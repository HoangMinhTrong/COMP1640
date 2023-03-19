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
            SeedUserDepartment(builder);
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
                    Name = EnumHelper.GetValue(RoleTypeEnum.Admin),
                    NormalizedName = EnumHelper.GetValue(RoleTypeEnum.Admin).ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.QAManager,
                    Name = EnumHelper.GetValue(RoleTypeEnum.QAManager),
                    NormalizedName = EnumHelper.GetValue(RoleTypeEnum.QAManager).ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.DepartmentQA,
                    Name = EnumHelper.GetValue(RoleTypeEnum.DepartmentQA),
                    NormalizedName = EnumHelper.GetValue(RoleTypeEnum.DepartmentQA).ToUpper()
                },
                new Role()
                {
                    Id = (int)RoleTypeEnum.Staff,
                    Name = EnumHelper.GetValue(RoleTypeEnum.Staff),
                    NormalizedName = EnumHelper.GetValue(RoleTypeEnum.Staff).ToUpper()
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
                UserName = "admin@qa.team",
                Email = "admin@qa.team",
                NormalizedEmail = "admin@qa.team".ToUpper(),
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "admin@qa.team".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // QA Manager 
            var director = new User
            {
                Id = 2,
                UserName = "qamanager@qa.team",
                Email = "qamanager@qa.team",
                NormalizedEmail = "qamanager@qa.team".ToUpper(),
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "qamanager@qa.team".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Department QA Coordinator 
            var computingDepartmentQA = new User
            {
                Id = 3,
                UserName = "computingdepartmentqa@qa.team",
                Email = "computingdepartmentqa@qa.team",
                NormalizedEmail = "computingdepartmentqa@qa.team".ToUpper(),
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "computingdepartmentqa@qa.team".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            
            // Department QA Coordinator 
            var businessDepartmentQA = new User
            {
                Id = 4,
                UserName = "businessDepartmentQA@qa.team",
                Email = "businessDepartmentQA@qa.team",
                NormalizedEmail = "businessDepartmentQA@qa.team".ToUpper(),
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "businessDepartmentQA@qa.team".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            
            // Department QA Coordinator 
            var designDepartmentQA = new User
            {
                Id = 5,
                UserName = "designDepartmentQA@qa.team",
                Email = "designDepartmentQA@qa.team",
                NormalizedEmail = "designDepartmentQA@qa.team".ToUpper(),
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "designDepartmentQA@qa.team".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Default@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // Staff 
            var staff = new User
            {
                Id = 6,
                UserName = "staff@qa.team",
                Email = "staff@qa.team",
                NormalizedEmail = "staff@qa.team".ToUpper(),
                Gender = UserGenderEnum.Male,
                NormalizedUserName = "staff@qa.team".ToUpper(),
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

        private static void SeedUserDepartment(ModelBuilder builder)
        {
            var userRoles = new List<UserDepartment>()
            {
                new UserDepartment()
                {
                    // QA Computing
                    UserId = 3,
                    DepartmentId = 1
                },
                new UserDepartment()
                {
                    // QA Business
                    UserId = 4,
                    DepartmentId = 2
                },
                new UserDepartment()
                {
                    // QA Design
                    UserId = 5,
                    DepartmentId = 3
                },
                new UserDepartment()
                {
                    // Staff Computing
                    UserId = 6,
                    DepartmentId = 1
                },
                
                new UserDepartment()
                {
                    // Admin Computing
                    UserId = 1,
                    DepartmentId = 1
                },
                
                new UserDepartment()
                {
                    // Admin Computing
                    UserId = 1,
                    DepartmentId = 2
                },
                
                new UserDepartment()
                {
                    // Admin Computing
                    UserId = 1,
                    DepartmentId = 3
                }
            };

            builder.Entity<UserDepartment>().HasData(userRoles);
        }
    }
}
