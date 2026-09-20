using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoGolf.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddClubPricingAndRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FromPrice",
                table: "Clubs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Clubs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromPrice",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Clubs");
        }
    }
}
