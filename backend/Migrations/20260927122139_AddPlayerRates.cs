using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoGolf.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Bays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlayerRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClubId = table.Column<int>(type: "integer", nullable: false),
                    PlayerCount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRate", x => x.Id);
                    table.CheckConstraint("CK_PlayerRate_PlayerCountPositive", "\"PlayerCount\" >= 1");
                    table.ForeignKey(
                        name: "FK_PlayerRate_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Bays",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "MaxPlayers" },
                values: new object[] { null, 1 });

            migrationBuilder.InsertData(
                table: "PlayerRate",
                columns: new[] { "Id", "ClubId", "PlayerCount", "Price" },
                values: new object[,]
                {
                    { 1, 1, 1, 150m },
                    { 2, 1, 2, 200m },
                    { 3, 1, 3, 250m },
                    { 4, 1, 4, 300m },
                    { 5, 2, 1, 150m },
                    { 6, 2, 2, 200m },
                    { 7, 2, 3, 250m },
                    { 8, 2, 4, 300m },
                    { 9, 3, 1, 150m },
                    { 10, 3, 2, 200m },
                    { 11, 3, 3, 250m },
                    { 12, 3, 4, 300m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRate_ClubId_PlayerCount",
                table: "PlayerRate",
                columns: new[] { "ClubId", "PlayerCount" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRate");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bays");

            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Bays");
        }
    }
}
