using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoGolf.Api.Migrations
{
    /// <inheritdoc />
    public partial class addOperatingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotDurationInMinutes",
                table: "Clubs",
                type: "integer",
                nullable: false,
                defaultValue: 60);

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneId",
                table: "Clubs",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "Africa/Johannesburg");

            migrationBuilder.CreateTable(
                name: "OperatingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClubId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    OpensAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ClosesAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingHours", x => x.Id);
                    table.CheckConstraint("CK_TradingDay_ClosesAfterOpens", "\"ClosesAt\" > \"OpensAt\"");
                    table.ForeignKey(
                        name: "FK_OperatingHours_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SlotDurationInMinutes", "TimeZoneId" },
                values: new object[] { 60, "Africa/Johannesburg" });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SlotDurationInMinutes", "TimeZoneId" },
                values: new object[] { 60, "Africa/Johannesburg" });

            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SlotDurationInMinutes", "TimeZoneId" },
                values: new object[] { 60, "Africa/Johannesburg" });

            migrationBuilder.InsertData(
                table: "OperatingHours",
                columns: new[] { "Id", "ClosesAt", "ClubId", "DayOfWeek", "OpensAt" },
                values: new object[,]
                {
                    { 1, new TimeOnly(22, 0, 0), 1, 0, new TimeOnly(8, 0, 0) },
                    { 2, new TimeOnly(22, 0, 0), 1, 1, new TimeOnly(8, 0, 0) },
                    { 3, new TimeOnly(22, 0, 0), 1, 2, new TimeOnly(8, 0, 0) },
                    { 4, new TimeOnly(22, 0, 0), 1, 3, new TimeOnly(8, 0, 0) },
                    { 5, new TimeOnly(22, 0, 0), 1, 4, new TimeOnly(8, 0, 0) },
                    { 6, new TimeOnly(22, 0, 0), 1, 5, new TimeOnly(8, 0, 0) },
                    { 7, new TimeOnly(22, 0, 0), 1, 6, new TimeOnly(8, 0, 0) },
                    { 8, new TimeOnly(21, 0, 0), 2, 0, new TimeOnly(7, 0, 0) },
                    { 9, new TimeOnly(21, 0, 0), 2, 2, new TimeOnly(7, 0, 0) },
                    { 10, new TimeOnly(21, 0, 0), 2, 3, new TimeOnly(7, 0, 0) },
                    { 11, new TimeOnly(21, 0, 0), 2, 4, new TimeOnly(7, 0, 0) },
                    { 12, new TimeOnly(21, 0, 0), 2, 5, new TimeOnly(7, 0, 0) },
                    { 13, new TimeOnly(21, 0, 0), 2, 6, new TimeOnly(7, 0, 0) },
                    { 14, new TimeOnly(22, 0, 0), 3, 6, new TimeOnly(9, 0, 0) },
                    { 15, new TimeOnly(22, 0, 0), 3, 0, new TimeOnly(9, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperatingHours_ClubId_DayOfWeek",
                table: "OperatingHours",
                columns: new[] { "ClubId", "DayOfWeek" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperatingHours");

            migrationBuilder.DropColumn(
                name: "SlotDurationInMinutes",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "TimeZoneId",
                table: "Clubs");
        }
    }
}
