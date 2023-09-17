using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    /// <inheritdoc />
    public partial class availableCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubjectCode",
                table: "MustCourses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DeptCode",
                table: "MustCourses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "AvailableCourses",
                columns: table => new
                {
                    AvailableCoursesId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectCode = table.Column<int>(type: "integer", nullable: true),
                    SubjectsSubjectCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableCourses", x => x.AvailableCoursesId);
                    table.ForeignKey(
                        name: "FK_AvailableCourses_Subjects_SubjectsSubjectCode",
                        column: x => x.SubjectsSubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode");
                });

            migrationBuilder.CreateTable(
                name: "ElectiveCourses",
                columns: table => new
                {
                    ElectiveCoursesId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ElectiveType = table.Column<int>(type: "integer", nullable: true),
                    SubjectCode = table.Column<int>(type: "integer", nullable: true),
                    AvailableCoursesId = table.Column<int>(type: "integer", nullable: true),
                    DeptCode = table.Column<int>(type: "integer", nullable: true),
                    DepartmentsDeptCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectiveCourses", x => x.ElectiveCoursesId);
                    table.ForeignKey(
                        name: "FK_ElectiveCourses_AvailableCourses_AvailableCoursesId",
                        column: x => x.AvailableCoursesId,
                        principalTable: "AvailableCourses",
                        principalColumn: "AvailableCoursesId");
                    table.ForeignKey(
                        name: "FK_ElectiveCourses_Departments_DepartmentsDeptCode",
                        column: x => x.DepartmentsDeptCode,
                        principalTable: "Departments",
                        principalColumn: "DeptCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableCourses_SubjectsSubjectCode",
                table: "AvailableCourses",
                column: "SubjectsSubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_ElectiveCourses_AvailableCoursesId",
                table: "ElectiveCourses",
                column: "AvailableCoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectiveCourses_DepartmentsDeptCode",
                table: "ElectiveCourses",
                column: "DepartmentsDeptCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectiveCourses");

            migrationBuilder.DropTable(
                name: "AvailableCourses");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectCode",
                table: "MustCourses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeptCode",
                table: "MustCourses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
