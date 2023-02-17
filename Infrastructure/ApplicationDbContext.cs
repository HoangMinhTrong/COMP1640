using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.SeedData;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Idea> Ideas { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<TenantUser> TenantUsers { get; set; }
        public virtual DbSet<UserDepartment> UserDepartments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            OnModelCreatingParial(builder);

            // Update default Asp table name
            RemoveDefaultAspTableName(builder);

            // Seeding data here
            var tenant = IdentitySeeder.Tenant;
            IdentitySeeder.Seeds(builder);
            DepartmentSeeder.Seeds(builder, tenant);
            CategorySeeder.Seeds(builder, tenant);

        }

        private void OnModelCreatingParial(ModelBuilder builder)
        {
            builder.Entity<TenantUser>().HasKey(tu => new { tu.UserId, tu.TenantId});

            builder.Entity<TenantUser>()
                .HasOne<User>(tu => tu.User)
                .WithMany(u => u.TenantUsers)
                .HasForeignKey(tu => tu.UserId);

            builder.Entity<TenantUser>()
                .HasOne<Tenant>(tu => tu.Tenant)
                .WithMany(t => t.TenantUsers)
                .HasForeignKey(tu => tu.TenantId);
            
            builder.Entity<UserDepartment>()
                .HasKey(ud => new {ud.UserId, ud.DepartmentId});
            
            builder.Entity<UserDepartment>()
                .HasOne<User>(ud => ud.User)
                .WithMany(u => u.UserDepartments)
                .HasForeignKey(tu => tu.UserId);

            builder.Entity<UserDepartment>()
                .HasOne<Department>(ud => ud.Department)
                .WithMany(t => t.UserDepartments)
                .HasForeignKey(tu => tu.DepartmentId);
        }

        public void RemoveDefaultAspTableName(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}