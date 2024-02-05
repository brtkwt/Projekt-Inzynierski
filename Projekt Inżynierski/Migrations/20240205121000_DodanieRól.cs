using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjektInżynierski.Migrations
{
    /// <inheritdoc />
    public partial class DodanieRól : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4db2f14a-6754-4455-b20e-66b19e5987e7", null, "Admin", "ADMIN" },
                    { "9676a52d-306f-458e-b21b-5c48184cc340", null, "Client", "CLIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4db2f14a-6754-4455-b20e-66b19e5987e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9676a52d-306f-458e-b21b-5c48184cc340");
        }
    }
}
