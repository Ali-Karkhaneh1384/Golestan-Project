using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class notMapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time_slot_id",
                table: "sections");
            migrationBuilder.DropColumn(
                name: "section_time",
                table: "sectionsId"
                );
            migrationBuilder.DropColumn(
                name: "section_time",
                table: "time_slotsId"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "time_slot_id",
                table: "sections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
