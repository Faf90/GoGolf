using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoGolf.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedClubData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FromPrice", "Rating" },
                values: new object[] { 150m, 5m });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FromPrice", "Rating" },
                values: new object[] { 200m, 5m });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FromPrice", "Rating" },
                values: new object[] { 185m, 4.7m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FromPrice", "Rating" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FromPrice", "Rating" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FromPrice", "Rating" },
                values: new object[] { 0m, 0m });
        }
    }
}
