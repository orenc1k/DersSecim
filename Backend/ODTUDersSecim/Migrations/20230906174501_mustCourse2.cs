using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    /// <inheritdoc />
    public partial class mustCourse2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectCode",
                table: "MustCourses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectsSubjectCode",
                table: "MustCourses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MustCourses_SubjectsSubjectCode",
                table: "MustCourses",
                column: "SubjectsSubjectCode");

            migrationBuilder.AddForeignKey(
                name: "FK_MustCourses_Subjects_SubjectsSubjectCode",
                table: "MustCourses",
                column: "SubjectsSubjectCode",
                principalTable: "Subjects",
                principalColumn: "SubjectCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MustCourses_Subjects_SubjectsSubjectCode",
                table: "MustCourses");

            migrationBuilder.DropIndex(
                name: "IX_MustCourses_SubjectsSubjectCode",
                table: "MustCourses");

            migrationBuilder.DropColumn(
                name: "SubjectCode",
                table: "MustCourses");

            migrationBuilder.DropColumn(
                name: "SubjectsSubjectCode",
                table: "MustCourses");
        }
    }
}
