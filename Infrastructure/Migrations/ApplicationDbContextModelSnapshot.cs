﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.AcademicYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ClosureDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FinalClosureDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AcademicYear");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Category 1",
                            TenantId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Category 2",
                            TenantId = 1
                        });
                });

            modelBuilder.Entity("Domain.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("QaCoordinatorId")
                        .HasColumnType("integer");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QaCoordinatorId")
                        .IsUnique();

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Computing",
                            QaCoordinatorId = 3,
                            TenantId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Business",
                            QaCoordinatorId = 4,
                            TenantId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Design",
                            QaCoordinatorId = 5,
                            TenantId = 1
                        });
                });

            modelBuilder.Entity("Domain.Idea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AcademicYearId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("boolean");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("Domain.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdeaId")
                        .HasColumnType("integer");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "aa101d8d-0c92-4bf6-920c-f61f17035477",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "082567cb-5dce-4c86-afed-7480f10d3d77",
                            Name = "University QA Manager",
                            NormalizedName = "UNIVERSITY QA MANAGER"
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyStamp = "94f534f7-85ea-4e52-a681-a7127d000d2c",
                            Name = "Department QA Coordinator",
                            NormalizedName = "DEPARTMENT QA COORDINATOR"
                        },
                        new
                        {
                            Id = 4,
                            ConcurrencyStamp = "7feec936-8625-4478-b1f9-6bc106843a5d",
                            Name = "Staff",
                            NormalizedName = "STAFF"
                        });
                });

            modelBuilder.Entity("Domain.RoleUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleUsers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 3,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 4,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 5,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 6,
                            RoleId = 4
                        });
                });

            modelBuilder.Entity("Domain.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Greenwich Danang"
                        });
                });

            modelBuilder.Entity("Domain.TenantUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantUsers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            TenantId = 1
                        },
                        new
                        {
                            UserId = 2,
                            TenantId = 1
                        },
                        new
                        {
                            UserId = 3,
                            TenantId = 1
                        },
                        new
                        {
                            UserId = 4,
                            TenantId = 1
                        },
                        new
                        {
                            UserId = 5,
                            TenantId = 1
                        },
                        new
                        {
                            UserId = 6,
                            TenantId = 1
                        });
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<byte?>("Gender")
                        .HasColumnType("smallint");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "863c08d7-d9c1-4a8d-b0a5-375d963b79dd",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            Gender = (byte)1,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEDr6gaTAcDiYWRq0FOgkRsv1i0YbjCCvSxUTzshkxzHdobuOzvPojas1IX2KqqlBPg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f17c8c99-8644-4b4e-8a1c-d9a2b7be9b6c",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "77e6bbdc-4e2f-4e46-abf0-aee119030a08",
                            Email = "qamanager@gmail.com",
                            EmailConfirmed = false,
                            Gender = (byte)1,
                            LockoutEnabled = false,
                            NormalizedEmail = "QAMANAGER@GMAIL.COM",
                            NormalizedUserName = "QAMANAGER@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEG4Y7AKC27wQP67k4vYlqxwhTTJ/1RungW59w8sQvj5d06AkgD+3sNxV5eosmH828A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ae2a74b1-95f6-48b3-8c36-64e66dd8704a",
                            TwoFactorEnabled = false,
                            UserName = "qamanager@gmail.com"
                        },
                        new
                        {
                            Id = 3,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "15194f31-441c-45db-8069-2ac6a9feea0f",
                            Email = "computingdepartmentqa@gmail.com",
                            EmailConfirmed = false,
                            Gender = (byte)1,
                            LockoutEnabled = false,
                            NormalizedEmail = "COMPUTINGDEPARTMENTQA@GMAIL.COM",
                            NormalizedUserName = "COMPUTINGDEPARTMENTQA@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEACgKLieCg5Ytpnf4HAOP0knq5URKszwreVFtdAFe/Ep7RcZhrzV7UX0lSjkiaQKug==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "864a101b-e424-4455-a56c-9a2edc61b0a2",
                            TwoFactorEnabled = false,
                            UserName = "computingdepartmentqa@gmail.com"
                        },
                        new
                        {
                            Id = 4,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0575423c-df0b-4e5f-8321-80f389d210e4",
                            Email = "businessDepartmentQA@gmail.com",
                            EmailConfirmed = false,
                            Gender = (byte)1,
                            LockoutEnabled = false,
                            NormalizedEmail = "BUSINESSDEPARTMENTQA@GMAIL.COM",
                            NormalizedUserName = "BUSINESSDEPARTMENTQA@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEG3GPIro7cXu1oQbbXC1ZsgGsxYazLXKUQXpuOhmzTvZQC6HAtbMK5KQuig94SwryA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e8cb1565-f863-4ead-8650-e9d6872bee03",
                            TwoFactorEnabled = false,
                            UserName = "businessDepartmentQA@gmail.com"
                        },
                        new
                        {
                            Id = 5,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d86ea9a3-ae5d-4d1c-bdf9-c297a827c28d",
                            Email = "designDepartmentQA@gmail.com",
                            EmailConfirmed = false,
                            Gender = (byte)1,
                            LockoutEnabled = false,
                            NormalizedEmail = "DESIGNDEPARTMENTQA@GMAIL.COM",
                            NormalizedUserName = "DESIGNDEPARTMENTQA@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEHuOrfRHWI6I456hnZ5sWZdZbMa9jQiL+RKeOg5e4PkO+LE0nuSk6rDj4xY0FoxoCg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "239fedfc-b904-4429-bcfb-a418b140ae37",
                            TwoFactorEnabled = false,
                            UserName = "designDepartmentQA@gmail.com"
                        },
                        new
                        {
                            Id = 6,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "804e69f7-d757-4336-ae2e-bb3a178855a0",
                            Email = "staff@gmail.com",
                            EmailConfirmed = false,
                            Gender = (byte)1,
                            LockoutEnabled = false,
                            NormalizedEmail = "STAFF@GMAIL.COM",
                            NormalizedUserName = "STAFF@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAENyZm4VSiZCb9O0N72tzb57ph4xa65xCWcVWvlomEQ+HiFEeydSOqIgJ1HjdpiHPaA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2bf4ee9c-97ab-4103-974b-8502b452f27a",
                            TwoFactorEnabled = false,
                            UserName = "staff@gmail.com"
                        });
                });

            modelBuilder.Entity("Domain.UserDepartment", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("UserDepartments");

                    b.HasData(
                        new
                        {
                            UserId = 3,
                            DepartmentId = 1
                        },
                        new
                        {
                            UserId = 4,
                            DepartmentId = 2
                        },
                        new
                        {
                            UserId = 5,
                            DepartmentId = 3
                        },
                        new
                        {
                            UserId = 6,
                            DepartmentId = 1
                        },
                        new
                        {
                            UserId = 1,
                            DepartmentId = 1
                        },
                        new
                        {
                            UserId = 1,
                            DepartmentId = 2
                        },
                        new
                        {
                            UserId = 1,
                            DepartmentId = 3
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Department", b =>
                {
                    b.HasOne("Domain.User", "QaCoordinator")
                        .WithOne()
                        .HasForeignKey("Domain.Department", "QaCoordinatorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("QaCoordinator");
                });

            modelBuilder.Entity("Domain.Idea", b =>
                {
                    b.HasOne("Domain.AcademicYear", "AcademicYear")
                        .WithMany("Ideas")
                        .HasForeignKey("AcademicYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Category", "Category")
                        .WithMany("Ideas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "CreatedByNavigation")
                        .WithMany("Ideas")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Domain.Department", "Department")
                        .WithMany("Ideas")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicYear");

                    b.Navigation("Category");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Domain.Reaction", b =>
                {
                    b.HasOne("Domain.Idea", "Idea")
                        .WithMany("Reactions")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("Reactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Idea");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.RoleUser", b =>
                {
                    b.HasOne("Domain.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("RoleUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.TenantUser", b =>
                {
                    b.HasOne("Domain.Tenant", "Tenant")
                        .WithMany("TenantUsers")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("TenantUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.UserDepartment", b =>
                {
                    b.HasOne("Domain.Department", "Department")
                        .WithMany("UserDepartments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("UserDepartments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.AcademicYear", b =>
                {
                    b.Navigation("Ideas");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Navigation("Ideas");
                });

            modelBuilder.Entity("Domain.Department", b =>
                {
                    b.Navigation("Ideas");

                    b.Navigation("UserDepartments");
                });

            modelBuilder.Entity("Domain.Idea", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("Domain.Tenant", b =>
                {
                    b.Navigation("TenantUsers");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Ideas");

                    b.Navigation("Reactions");

                    b.Navigation("RoleUsers");

                    b.Navigation("TenantUsers");

                    b.Navigation("UserDepartments");
                });
#pragma warning restore 612, 618
        }
    }
}
