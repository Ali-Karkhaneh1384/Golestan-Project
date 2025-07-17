using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   
            migrationBuilder.DropForeignKey(
                name: "FK_teaches_instructors_instructor_id",
                table: "teaches");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_users_instructor_id",
                table: "instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_instructors",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "instructor_id",
                table: "instructors");

            migrationBuilder.AddColumn<int>(
                name: "instructor_id",
                table: "instructors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_instructors",
                table: "instructors",
                column: "instructor_id");

         
            migrationBuilder.CreateIndex(
                name: "IX_instructors_user_id",
                table: "instructors",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_users_user_id",
                table: "instructors",
                column: "user_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            
            migrationBuilder.AddForeignKey(
                name: "FK_teaches_instructors_instructor_id",
                table: "teaches",
                column: "instructor_id",
                principalTable: "instructors",
                principalColumn: "instructor_id",
                onDelete: ReferentialAction.Cascade);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_users_user_id",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_instructors_user_id",
                table: "instructors");

            migrationBuilder.AlterColumn<int>(
                name: "instructor_id",
                table: "instructors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_users_instructor_id",
                table: "instructors",
                column: "instructor_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
