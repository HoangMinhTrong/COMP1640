using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Idea> Ideas { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Idea>(entity =>
            {
                entity.ToTable("Idea");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Idea_User");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Idea_Department");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.ToTable("Reaction");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reaction_Idea");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reaction_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(d => d.Departments)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentUser",
                        l => l.HasOne<Department>().WithMany().HasForeignKey("DepartmentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DepartmentUser_Department"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DepartmentUser_User"),
                        j =>
                        {
                            j.HasKey("UserId", "DepartmentId");

                            j.ToTable("DepartmentUser");
                        });

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRole_Role"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRole_User"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("UserRole");
                        });

                entity.HasMany(d => d.Tenants)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "TenantUser",
                        l => l.HasOne<Tenant>().WithMany().HasForeignKey("TenantId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TenantUser_Tenant"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TenantUser_User"),
                        j =>
                        {
                            j.HasKey("UserId", "TenantId");

                            j.ToTable("TenantUser");
                        });
            });
        }
    }
}