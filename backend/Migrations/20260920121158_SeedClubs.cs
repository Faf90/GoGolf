using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoGolf.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Moregloed, Pretoria", "Shank" },
                    { 2, "Montana, Pretoria", "Simpact" },
                    { 3, "Fourways, Johannesburg", "Golf Bar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
