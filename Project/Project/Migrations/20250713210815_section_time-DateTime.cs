using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class section_timeDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sections_timeslots_time_slot_id",
                table: "sections");

            migrationBuilder.DropIndex(
                name: "IX_sections_time_slot_id",
                table: "sections");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                table: "timeslots",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_time",
                table: "timeslots",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.CreateTable(
                name: "section_Times",
                columns: table => new
                {
                    section_id = table.Column<int>(type: "int", nullable: false),
                    time_slot_id = table.Column<int>(type: "int", nullable: false),
                    sectionsId = table.Column<int>(type: "int", nullable: true),
                    time_slotsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_section_Times", x => new { x.section_id, x.time_slot_id });
                    table.ForeignKey(
                        name: "FK_section_Times_sections_sectionsId",
                        column: x => x.sectionsId,
                        principalTable: "sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_section_Times_timeslots_time_slotsId",
                        column: x => x.time_slotsId,
                        principalTable: "timeslots",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new DateTime(1, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_section_Times_sectionsId",
                table: "section_Times",
                column: "sectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_section_Times_time_slotsId",
                table: "section_Times",
                column: "time_slotsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "section_Times");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "start_time",
                table: "timeslots",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "end_time",
                table: "timeslots",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 10, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "timeslots",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "end_time", "start_time" },
                values: new object[] { new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_sections_time_slot_id",
                table: "sections",
                column: "time_slot_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sections_timeslots_time_slot_id",
                table: "sections",
                column: "time_slot_id",
                principalTable: "timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
