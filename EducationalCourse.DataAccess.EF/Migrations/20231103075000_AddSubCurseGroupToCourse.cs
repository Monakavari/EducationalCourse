using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalCourse.DataAccess.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddSubCurseGroupToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCourseGroupId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubCourseGroupId",
                table: "Courses",
                column: "SubCourseGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_SubCourseGroupId",
                table: "Courses",
                column: "SubCourseGroupId",
                principalTable: "CourseGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_SubCourseGroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SubCourseGroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SubCourseGroupId",
                table: "Courses");
        }
    }
}
