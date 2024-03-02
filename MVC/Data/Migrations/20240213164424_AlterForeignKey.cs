using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class AlterForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesResults_Departments_DepartId",
                table: "CoursesResults");

            migrationBuilder.RenameColumn(
                name: "DepartId",
                table: "CoursesResults",
                newName: "TranieeId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesResults_DepartId",
                table: "CoursesResults",
                newName: "IX_CoursesResults_TranieeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesResults_Trainees_TranieeId",
                table: "CoursesResults",
                column: "TranieeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesResults_Trainees_TranieeId",
                table: "CoursesResults");

            migrationBuilder.RenameColumn(
                name: "TranieeId",
                table: "CoursesResults",
                newName: "DepartId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesResults_TranieeId",
                table: "CoursesResults",
                newName: "IX_CoursesResults_DepartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesResults_Departments_DepartId",
                table: "CoursesResults",
                column: "DepartId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
