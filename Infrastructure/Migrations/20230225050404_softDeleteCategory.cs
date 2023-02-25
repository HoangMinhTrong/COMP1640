using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class softDeleteCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "574f3f3a-426c-41f1-8185-1d1fc051de2a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "af70a0d9-1d0e-465c-9a8c-ca7b70c64ccb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8affaab8-7404-43c4-bb85-bea831842659");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0584a636-1048-47cf-a7d0-f7e77b1e0968");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0553ce04-eef2-45dc-90c9-cbad114c71c7", "AQAAAAEAACcQAAAAEHfrq3mf0SucYvbHHao3ozPjdkiAjLtFyHMlqIA/6WzCWPMo8Ggani0ObVcahVu3cg==", "3a647703-7249-4e81-802e-15e2d6f17c3c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f48cd92-d894-4def-b7c7-82e035add37e", "AQAAAAEAACcQAAAAEInD+zsRV1Sou3IXjkmLBkl0niV9SmJIsH005bgFlYPXO1XFz/Xc333F/kbPufwsmg==", "02d7bfa3-d3ee-4edb-ae4a-ed66f6da3db6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1e8f42a-fcbe-475c-99b9-5b82e6471d3b", "AQAAAAEAACcQAAAAEP3MX0wEQfH9QBmo+H5Gk21TpGjJOALI/Ib9JukP6urvPNXg2IStxQ6lEawbaRxk+g==", "f435ee84-6dbd-4982-90a4-e9c66a58200d" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bed1a9c8-c671-4eac-acdf-9c32dcbc012f", "AQAAAAEAACcQAAAAEMN+Mf9DVmioDTTJMRLsZQY25WZeRlELQ/WqDB8E25kvOXRFXCtx7GVQifmyyWYZKg==", "861c21f3-1a82-42c6-b203-43b1683a69b3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d012fe0-fe3f-449d-8007-8e0e1ec0ad7a", "AQAAAAEAACcQAAAAEJnSSwtchFtIKH7NqN2Cg6PWVfk1SEdRGvs869X/WgxpfybditkCX7nK6bWsFLFiUg==", "1321fc75-6817-4779-b29c-7bc8a38adbbb" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e034e53-1bf2-408a-8fd1-f9be5a0dac61", "AQAAAAEAACcQAAAAEC8HwigEE5L7Ztedb/Ekct5IJoInB4iwROqnh9q5FcOeQn7fuT4fr/cU7GE4nYLlmQ==", "d1f7e6d2-2580-40b5-af9e-462d3bb556f0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3a6e2cda-00de-4167-bccc-d0e38611885b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8f3efbed-9c1f-4a2d-9383-fcdaf1218f9a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3df6f413-2c25-458b-bbbf-05d2e86aeba9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "cf76ea7c-b927-4c6a-b7d7-3528bef2c5bd");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcf5749c-86da-41da-81cf-e0065c06a4a5", "AQAAAAEAACcQAAAAENi1Drams+LP2dbfM03zYb3fGW5pkBceJd4o97ikWMB3lZ3HRpFqOiyyvJlkxvwplQ==", "fe9e2ba5-d936-4a06-97f0-4fdde3f7a6e4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4303677b-9674-4749-8c21-aced60eee478", "AQAAAAEAACcQAAAAEN5YjvJpu9kxEID27OvKramVOhirvQUoUKerUmMypmMP64OSPKFmoJTbWd5no9W7rg==", "650230d1-433c-4343-903f-ef86ffd21ec8" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6684673-0b14-4c91-b7a1-34b026b9755b", "AQAAAAEAACcQAAAAEFVXA6ANAsXbtU5VXq6jHDqn8syULcPWq8hLdxLJ7hUyMvXlhQcswL+68kakMaziaQ==", "a326befe-5a26-4eaf-9f66-71fa0bf88585" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91efa4cd-5057-4741-85c4-8f50f80ce5b1", "AQAAAAEAACcQAAAAEB3gZDJtHcuBx9D3hEGD76KxRQa5cPt9OPWSgJaXLSiVziTcmVPRUGCgIRKpAypYSw==", "4a06586c-e0b9-405a-be5f-c17ce1c30714" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80eb30a3-00db-4087-8817-6bae00a57fc3", "AQAAAAEAACcQAAAAEHXOJfx+fR1CZVM8s6vbvmkFBQS0aNE0pBMnVRhDSyLaJrItmzM9ZX5t904F1/UKRA==", "6db480a2-57ed-49f1-a4d4-050dd7efa069" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e1d3c50-9e57-4b9a-a2e0-ad7b788f0860", "AQAAAAEAACcQAAAAEPNymsPEQlr19hglHXJeqXw5Lzn/Z4mRBkVWcwQGxbb5uZDahArTuVWRQwoovE/a+Q==", "c68dd8bd-428a-4bb0-8f80-f31636da8544" });
        }
    }
}
