using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class extraColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_section_Times_sections_sectionsId",
                table: "section_Times");

            migrationBuilder.DropForeignKey(
                name: "FK_section_Times_timeslots_time_slotsId",
                table: "section_Times");

            migrationBuilder.DropIndex(
                name: "IX_section_Times_sectionsId",
                table: "section_Times");

            migrationBuilder.DropIndex(
                name: "IX_section_Times_time_slotsId",
                table: "section_Times");

            migrationBuilder.DropColumn(
                name: "sectionsId",
                table: "section_Times");

            migrationBuilder.DropColumn(
                name: "time_slotsId",
                table: "section_Times");

            migrationBuilder.CreateIndex(
                name: "IX_section_Times_time_slot_id",
                table: "section_Times",
                column: "time_slot_id");

            migrationBuilder.AddForeignKey(
                name: "FK_section_Times_sections_section_id",
                table: "section_Times",
                column: "section_id",
                principalTable: "sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_section_Times_timeslots_time_slot_id",
                table: "section_Times",
                column: "time_slot_id",
                principalTable: "timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_section_Times_sections_section_id",
                table: "section_Times");

            migrationBuilder.DropForeignKey(
                name: "FK_section_Times_timeslots_time_slot_id",
                table: "section_Times");

            migrationBuilder.DropIndex(
                name: "IX_section_Times_time_slot_id",
                table: "section_Times");

            migrationBuilder.AddColumn<int>(
                name: "sectionsId",
                table: "section_Times",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "time_slotsId",
                table: "section_Times",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_section_Times_sectionsId",
                table: "section_Times",
                column: "sectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_section_Times_time_slotsId",
                table: "section_Times",
                column: "time_slotsId");

            migrationBuilder.AddForeignKey(
                name: "FK_section_Times_sections_sectionsId",
                table: "section_Times",
                column: "sectionsId",
                principalTable: "sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_section_Times_timeslots_time_slotsId",
                table: "section_Times",
                column: "time_slotsId",
                principalTable: "timeslots",
                principalColumn: "Id");
        }
    }
}
