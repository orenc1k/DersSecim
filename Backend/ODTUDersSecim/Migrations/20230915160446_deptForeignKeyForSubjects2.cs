using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    /// <inheritdoc />
    public partial class deptForeignKeyForSubjects2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeptShortName = table.Column<string>(type: "text", nullable: true),
                    DeptFullName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptCode);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubjectCredit = table.Column<float>(type: "real", nullable: true),
                    SubjectLevel = table.Column<string>(type: "text", nullable: true),
                    SubjectType = table.Column<string>(type: "text", nullable: true),
                    EctsCredit = table.Column<float>(type: "real", nullable: true),
                    DeptCode = table.Column<int>(type: "integer", nullable: true),
                    DepartmentsDeptCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectCode);
                    table.ForeignKey(
                        name: "FK_Subjects_Departments_DepartmentsDeptCode",
                        column: x => x.DepartmentsDeptCode,
                        principalTable: "Departments",
                        principalColumn: "DeptCode");
                });

            migrationBuilder.CreateTable(
                name: "MustCourses",
                columns: table => new
                {
                    MustCourseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    DeptCode = table.Column<int>(type: "integer", nullable: false),
                    DepartmentsDeptCode = table.Column<int>(type: "integer", nullable: true),
                    SubjectCode = table.Column<int>(type: "integer", nullable: false),
                    SubjectsSubjectCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MustCourses", x => x.MustCourseId);
                    table.ForeignKey(
                        name: "FK_MustCourses_Departments_DepartmentsDeptCode",
                        column: x => x.DepartmentsDeptCode,
                        principalTable: "Departments",
                        principalColumn: "DeptCode");
                    table.ForeignKey(
                        name: "FK_MustCourses_Subjects_SubjectsSubjectCode",
                        column: x => x.SubjectsSubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode");
                });

            migrationBuilder.CreateTable(
                name: "SubjectSections",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SectionCode = table.Column<int>(type: "integer", nullable: false),
                    GivenDept = table.Column<string>(type: "text", nullable: true),
                    StartChar = table.Column<string>(type: "text", nullable: true),
                    EndChar = table.Column<string>(type: "text", nullable: true),
                    MinCumGpa = table.Column<float>(type: "real", nullable: true),
                    MaxCumGpa = table.Column<float>(type: "real", nullable: true),
                    MinYear = table.Column<int>(type: "integer", nullable: true),
                    MaxYear = table.Column<int>(type: "integer", nullable: true),
                    StartGrade = table.Column<string>(type: "text", nullable: true),
                    EndGrade = table.Column<string>(type: "text", nullable: true),
                    SubjectCode = table.Column<int>(type: "integer", nullable: true),
                    SubjectsSubjectCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectSections", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK_SubjectSections_Subjects_SubjectsSubjectCode",
                        column: x => x.SubjectsSubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode");
                });

            migrationBuilder.CreateTable(
                name: "SectionDays",
                columns: table => new
                {
                    SectionDaysId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day1 = table.Column<string>(type: "text", nullable: true),
                    Time1 = table.Column<string>(type: "text", nullable: true),
                    Day2 = table.Column<string>(type: "text", nullable: true),
                    Time2 = table.Column<string>(type: "text", nullable: true),
                    Day3 = table.Column<string>(type: "text", nullable: true),
                    Time3 = table.Column<string>(type: "text", nullable: true),
                    Place = table.Column<string>(type: "text", nullable: true),
                    InstructorName = table.Column<string>(type: "text", nullable: true),
                    SectionId = table.Column<int>(type: "integer", nullable: true),
                    SubjectSectionsSectionId = table.Column<int>(type: "integer", nullable: true),
                    SubjectCode = table.Column<int>(type: "integer", nullable: true),
                    SubjectsSubjectCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionDays", x => x.SectionDaysId);
                    table.ForeignKey(
                        name: "FK_SectionDays_SubjectSections_SubjectSectionsSectionId",
                        column: x => x.SubjectSectionsSectionId,
                        principalTable: "SubjectSections",
                        principalColumn: "SectionId");
                    table.ForeignKey(
                        name: "FK_SectionDays_Subjects_SubjectsSubjectCode",
                        column: x => x.SubjectsSubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MustCourses_DepartmentsDeptCode",
                table: "MustCourses",
                column: "DepartmentsDeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_MustCourses_SubjectsSubjectCode",
                table: "MustCourses",
                column: "SubjectsSubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_SectionDays_SubjectSectionsSectionId",
                table: "SectionDays",
                column: "SubjectSectionsSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionDays_SubjectsSubjectCode",
                table: "SectionDays",
                column: "SubjectsSubjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DepartmentsDeptCode",
                table: "Subjects",
                column: "DepartmentsDeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSections_SubjectsSubjectCode",
                table: "SubjectSections",
                column: "SubjectsSubjectCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MustCourses");

            migrationBuilder.DropTable(
                name: "SectionDays");

            migrationBuilder.DropTable(
                name: "SubjectSections");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
