using Domain;
using Infrastructure.Configurations;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Idea> Ideas { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<TenantUser> TenantUsers { get; set; }
        public virtual DbSet<UserDepartment> UserDepartments { get; set; }
        public virtual DbSet<RoleUser> RoleUsers { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<IdeaAttachment> IdeaAttachments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            OnModelCreatingParial(builder);

            // Update default Asp table name
            RemoveDefaultAspTableName(builder);

            // Seeding data here
            var tenant = IdentitySeeder.Tenant;
            IdentitySeeder.Seeds(builder);
            AcademicYearSeeder.Seeds(builder, tenant);
            DepartmentSeeder.Seeds(builder, tenant);
            CategorySeeder.Seeds(builder, tenant);
        }

        private void OnModelCreatingParial(ModelBuilder builder)
        {
            builder.Entity<TenantUser>()
                .HasKey(tu => new { tu.UserId, tu.TenantId });

            builder.Entity<TenantUser>()
                .HasOne<User>(tu => tu.User)
                .WithMany(u => u.TenantUsers)
                .HasForeignKey(tu => tu.UserId);

            builder.Entity<TenantUser>()
                .HasOne<Tenant>(tu => tu.Tenant)
                .WithMany(t => t.TenantUsers)
                .HasForeignKey(tu => tu.TenantId);

            builder.Entity<UserDepartment>()
                .HasKey(ud => new { ud.UserId, ud.DepartmentId });

            builder.Entity<UserDepartment>()
                .HasOne<User>(ud => ud.User)
                .WithMany(u => u.UserDepartments)
                .HasForeignKey(tu => tu.UserId);

            builder.Entity<UserDepartment>()
                .HasOne<Department>(ud => ud.Department)
                .WithMany(t => t.UserDepartments)
                .HasForeignKey(tu => tu.DepartmentId);

            builder.Entity<RoleUser>()
                .HasKey(ru => new { ru.UserId, ru.RoleId });

            builder.Entity<RoleUser>()
                .HasOne<User>(ru => ru.User)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(ru => ru.UserId);

            builder.Entity<RoleUser>()
                .HasOne<Role>(ru => ru.Role)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(ru => ru.RoleId);

            builder.Entity<Department>()
                .HasOne(d => d.QaCoordinator)
                .WithOne()
                .HasForeignKey<Department>(d => d.QaCoordinatorId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<IdeaAttachment>()
               .HasKey(ia => new { ia.IdeaId, ia.AttachmentId });

            builder.Entity<IdeaAttachment>()
                .HasOne<Idea>(ia => ia.Idea)
                .WithMany(i => i.IdeaAttachments)
                .HasForeignKey(ia => ia.IdeaId);

            builder.Entity<IdeaAttachment>()
                .HasOne<Attachment>(ia => ia.Attachment)
                .WithMany(a => a.IdeaAttachments)
                .HasForeignKey(ia => ia.AttachmentId);

            builder.ApplyConfiguration(new IdeaConfiguration());
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