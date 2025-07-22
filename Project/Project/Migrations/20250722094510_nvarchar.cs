using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class nvarchar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Last_Name",
                table: "users",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "First_Name",
                table: "users",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "timeslots",
                type: "nvarchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(225)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "courses",
                type: "nvarchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(225)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "courses",
                type: "nvarchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(225)");

            migrationBuilder.AlterColumn<string>(
                name: "building",
                table: "classrooms",
                type: "nvarchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(225)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Last_Name",
                table: "users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "First_Name",
                table: "users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "timeslots",
                type: "varchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(225)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "courses",
                type: "varchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(225)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "courses",
                type: "varchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(225)");

            migrationBuilder.AlterColumn<string>(
                name: "building",
                table: "classrooms",
                type: "varchar(225)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(225)");
        }
    }
}
