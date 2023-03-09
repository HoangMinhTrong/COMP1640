using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ClosureDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FinalClosureDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    KeyName = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Gender = table.Column<byte>(type: "smallint", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    QaCoordinatorId = table.Column<int>(type: "integer", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Users_QaCoordinatorId",
                        column: x => x.QaCoordinatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RoleUsers",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsers", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUsers", x => new { x.UserId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_TenantUsers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    AcademicYearId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeactive = table.Column<bool>(type: "boolean", nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ideas_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ideas_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ideas_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ideas_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserDepartments",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartments", x => new { x.UserId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_UserDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDepartments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdeaId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "IdeaAttachments",
                columns: table => new
                {
                    IdeaId = table.Column<int>(type: "integer", nullable: false),
                    AttachmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaAttachments", x => new { x.IdeaId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_IdeaAttachments_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeaAttachments_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdeaId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AcademicYear",
                columns: new[] { "Id", "ClosureDate", "EndDate", "FinalClosureDate", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 29, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 4, 30, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 29, 17, 0, 0, 0, DateTimeKind.Utc), "2022 - 2023", 1 },
                    { 2, new DateTime(2023, 11, 29, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 5, 31, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 24, 17, 0, 0, 0, DateTimeKind.Utc), "2023 - 2024", 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, false, "Category 1", 1 },
                    { 2, false, "Category 2", 1 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "e3957b01-4596-4398-8433-8b4ba92d2e21", "Admin", "ADMIN" },
                    { 2, "51c5e17d-c3fa-4a69-a1f2-d5377bfd7f25", "University QA Manager", "UNIVERSITY QA MANAGER" },
                    { 3, "345040c4-0bd2-4254-b3ba-7a204de9d31b", "Department QA Coordinator", "DEPARTMENT QA COORDINATOR" },
                    { 4, "5b96086d-20fc-4117-aa2f-301483352180", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Greenwich Danang" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "fb099031-4747-46b9-a3c2-9e068a08012f", "admin@gmail.com", false, (byte)1, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEGL4SGW+XhubzPKzd++VIql8CAu+aTO873IUptvbk8WEyOqIdT9addJxWfoHThpi1g==", null, false, "6dbae9e4-b111-4f22-ae59-bb101fb85b1b", false, "admin@gmail.com" },
                    { 2, 0, null, "b0a887af-9132-43bb-b1ce-270d63c1ed10", "qamanager@gmail.com", false, (byte)1, false, null, "QAMANAGER@GMAIL.COM", "QAMANAGER@GMAIL.COM", "AQAAAAEAACcQAAAAEEP6m10rGZCfT8p1PomWJmDy+aRl60pDbwU8y40gnEXw471emHX2aEBHQAmsGGcS7Q==", null, false, "4ae9dbfc-68c2-4331-b126-48cb36f8938f", false, "qamanager@gmail.com" },
                    { 3, 0, null, "97feb5a4-1aec-4329-9928-e4ef67650683", "computingdepartmentqa@gmail.com", false, (byte)1, false, null, "COMPUTINGDEPARTMENTQA@GMAIL.COM", "COMPUTINGDEPARTMENTQA@GMAIL.COM", "AQAAAAEAACcQAAAAEG+JDR/rYe5Qar+ZnuIGRSGizkTGPA9z+vfeeWBu2MHI2i2SnNPFXod7GMZVauu7hg==", null, false, "93107242-cf07-42d9-9b5c-ab37a16e99d2", false, "computingdepartmentqa@gmail.com" },
                    { 4, 0, null, "e33c171f-21af-40cf-8563-5cb2d61abad7", "businessDepartmentQA@gmail.com", false, (byte)1, false, null, "BUSINESSDEPARTMENTQA@GMAIL.COM", "BUSINESSDEPARTMENTQA@GMAIL.COM", "AQAAAAEAACcQAAAAELzMI7j30PFDN+V6+LlthOr2VIeJzHl1BLe6K94J5xJ9M8EXUq7i0tEv27ArHlqZzw==", null, false, "6176447e-b530-4ab9-b9dd-763e85f761b6", false, "businessDepartmentQA@gmail.com" },
                    { 5, 0, null, "ac69a8d2-3648-4637-a414-ad05beb88800", "designDepartmentQA@gmail.com", false, (byte)1, false, null, "DESIGNDEPARTMENTQA@GMAIL.COM", "DESIGNDEPARTMENTQA@GMAIL.COM", "AQAAAAEAACcQAAAAEB07JMjm+tNn0+6eakTWbLpgbyI3Z6eqPBEblFYzl0swFBHefHxlx/3hg6hi5JbngA==", null, false, "876c2069-0d17-4b80-84bc-9a3e52350c80", false, "designDepartmentQA@gmail.com" },
                    { 6, 0, null, "cd7edea3-fa78-4e06-9818-1eea432e0cf5", "staff@gmail.com", false, (byte)1, false, null, "STAFF@GMAIL.COM", "STAFF@GMAIL.COM", "AQAAAAEAACcQAAAAEAllfllAN1cErApmEkf0JaNUqwtgvcLQUHqmhHhDqcsI5ld5Yd8HzE8EV0OEhZX1hw==", null, false, "a8f70484-4c33-401b-9b2a-dba0b91b6333", false, "staff@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IsDeleted", "Name", "QaCoordinatorId", "TenantId" },
                values: new object[,]
                {
                    { 1, false, "Computing", 3, 1 },
                    { 2, false, "Business", 4, 1 },
                    { 3, false, "Design", 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "RoleUsers",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 4, 6 }
                });

            migrationBuilder.InsertData(
                table: "TenantUsers",
                columns: new[] { "TenantId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 }
                });

            migrationBuilder.InsertData(
                table: "UserDepartments",
                columns: new[] { "DepartmentId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 3 },
                    { 2, 4 },
                    { 3, 5 },
                    { 1, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedBy",
                table: "Comments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdeaId",
                table: "Comments",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_QaCoordinatorId",
                table: "Departments",
                column: "QaCoordinatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdeaAttachments_AttachmentId",
                table: "IdeaAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_AcademicYearId",
                table: "Ideas",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_CategoryId",
                table: "Ideas",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_CreatedBy",
                table: "Ideas",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_DepartmentId",
                table: "Ideas",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_IdeaId",
                table: "Reactions",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId",
                table: "Reactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsers_RoleId",
                table: "RoleUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUsers_TenantId",
                table: "TenantUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartments_DepartmentId",
                table: "UserDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "IdeaAttachments");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RoleUsers");

            migrationBuilder.DropTable(
                name: "TenantUsers");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserDepartments");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Ideas");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
