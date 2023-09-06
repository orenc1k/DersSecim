using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    /// <inheritdoc />
    public partial class mustCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubjectCode",
                table: "SectionDays",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "SectionDays",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "MustCourses",
                columns: table => new
                {
                    MustCourseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    CourseName = table.Column<string>(type: "text", nullable: false),
                    DeptCode = table.Column<int>(type: "integer", nullable: false),
                    DepartmentsDeptCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MustCourses", x => x.MustCourseId);
                    table.ForeignKey(
                        name: "FK_MustCourses_Departments_DepartmentsDeptCode",
                        column: x => x.DepartmentsDeptCode,
                        principalTable: "Departments",
                        principalColumn: "DeptCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MustCourses_DepartmentsDeptCode",
                table: "MustCourses",
                column: "DepartmentsDeptCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MustCourses");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectCode",
                table: "SectionDays",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "SectionDays",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
