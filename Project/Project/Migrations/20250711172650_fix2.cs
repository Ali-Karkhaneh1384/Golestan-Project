using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_takes_section_id",
                table: "takes");

            migrationBuilder.CreateIndex(
                name: "IX_takes_section_id",
                table: "takes",
                column: "section_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_takes_section_id",
                table: "takes");

            migrationBuilder.CreateIndex(
                name: "IX_takes_section_id",
                table: "takes",
                column: "section_id",
                unique: true);
        }
    }
}
