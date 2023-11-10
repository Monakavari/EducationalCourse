using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalCourse.DataAccess.EF.Migrations
{
    /// <inheritdoc />
    public partial class rev01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseComment_CourseComment_ParentId",
                table: "CourseComment");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseComment_Courses_CourseId",
                table: "CourseComment");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseComment_Users_UserId",
                table: "CourseComment");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisode_Courses_CourseId",
                table: "CourseEpisode");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLevel_CourseLevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatus_CourseStetusId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStatus",
                table: "CourseStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLevel",
                table: "CourseLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEpisode",
                table: "CourseEpisode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseComment",
                table: "CourseComment");

            migrationBuilder.RenameTable(
                name: "CourseStatus",
                newName: "CourseStatuses");

            migrationBuilder.RenameTable(
                name: "CourseLevel",
                newName: "CourseLeveles");

            migrationBuilder.RenameTable(
                name: "CourseEpisode",
                newName: "CourseEpisodes");

            migrationBuilder.RenameTable(
                name: "CourseComment",
                newName: "CourseComments");

            migrationBuilder.RenameColumn(
                name: "CourseStetusId",
                table: "Courses",
                newName: "CourseStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseStetusId",
                table: "Courses",
                newName: "IX_Courses_CourseStatusId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "CourseEpisodes",
                newName: "EpisodeFileTitle");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEpisode_CourseId",
                table: "CourseEpisodes",
                newName: "IX_CourseEpisodes_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseComment_UserId",
                table: "CourseComments",
                newName: "IX_CourseComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseComment_ParentId",
                table: "CourseComments",
                newName: "IX_CourseComments_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseComment_CourseId",
                table: "CourseComments",
                newName: "IX_CourseComments_CourseId");

            migrationBuilder.AddColumn<string>(
                name: "DemoVideo",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStatuses",
                table: "CourseStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLeveles",
                table: "CourseLeveles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEpisodes",
                table: "CourseEpisodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseComments",
                table: "CourseComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseComments_CourseComments_ParentId",
                table: "CourseComments",
                column: "ParentId",
                principalTable: "CourseComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseComments_Courses_CourseId",
                table: "CourseComments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseComments_Users_UserId",
                table: "CourseComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseId",
                table: "CourseEpisodes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLeveles_CourseLevelId",
                table: "Courses",
                column: "CourseLevelId",
                principalTable: "CourseLeveles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusId",
                table: "Courses",
                column: "CourseStatusId",
                principalTable: "CourseStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseComments_CourseComments_ParentId",
                table: "CourseComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseComments_Courses_CourseId",
                table: "CourseComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseComments_Users_UserId",
                table: "CourseComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseId",
                table: "CourseEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLeveles_CourseLevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStatuses",
                table: "CourseStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLeveles",
                table: "CourseLeveles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEpisodes",
                table: "CourseEpisodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseComments",
                table: "CourseComments");

            migrationBuilder.DropColumn(
                name: "DemoVideo",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "CourseStatuses",
                newName: "CourseStatus");

            migrationBuilder.RenameTable(
                name: "CourseLeveles",
                newName: "CourseLevel");

            migrationBuilder.RenameTable(
                name: "CourseEpisodes",
                newName: "CourseEpisode");

            migrationBuilder.RenameTable(
                name: "CourseComments",
                newName: "CourseComment");

            migrationBuilder.RenameColumn(
                name: "CourseStatusId",
                table: "Courses",
                newName: "CourseStetusId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseStatusId",
                table: "Courses",
                newName: "IX_Courses_CourseStetusId");

            migrationBuilder.RenameColumn(
                name: "EpisodeFileTitle",
                table: "CourseEpisode",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEpisodes_CourseId",
                table: "CourseEpisode",
                newName: "IX_CourseEpisode_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseComments_UserId",
                table: "CourseComment",
                newName: "IX_CourseComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseComments_ParentId",
                table: "CourseComment",
                newName: "IX_CourseComment_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseComments_CourseId",
                table: "CourseComment",
                newName: "IX_CourseComment_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStatus",
                table: "CourseStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLevel",
                table: "CourseLevel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEpisode",
                table: "CourseEpisode",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseComment",
                table: "CourseComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseComment_CourseComment_ParentId",
                table: "CourseComment",
                column: "ParentId",
                principalTable: "CourseComment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseComment_Courses_CourseId",
                table: "CourseComment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseComment_Users_UserId",
                table: "CourseComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisode_Courses_CourseId",
                table: "CourseEpisode",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLevel_CourseLevelId",
                table: "Courses",
                column: "CourseLevelId",
                principalTable: "CourseLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatus_CourseStetusId",
                table: "Courses",
                column: "CourseStetusId",
                principalTable: "CourseStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
