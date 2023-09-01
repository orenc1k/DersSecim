using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AEESubjects",
                columns: table => new
                {
                    SubjectCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "text", nullable: true),
                    SubjectCredit = table.Column<int>(type: "integer", nullable: true),
                    SubjectLevel = table.Column<string>(type: "text", nullable: true),
                    SubjectType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEESubjects", x => x.SubjectCode);
                });

            migrationBuilder.CreateTable(
                name: "CengSubjects",
                columns: table => new
                {
                    SubjectCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "text", nullable: true),
                    SubjectCredit = table.Column<int>(type: "integer", nullable: true),
                    SubjectLevel = table.Column<string>(type: "text", nullable: true),
                    SubjectType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CengSubjects", x => x.SubjectCode);
                });

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
                    SubjectName = table.Column<string>(type: "text", nullable: true),
                    SubjectCredit = table.Column<int>(type: "integer", nullable: true),
                    SubjectLevel = table.Column<string>(type: "text", nullable: true),
                    SubjectType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectCode);
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
                    SubjectCode = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSections_SubjectsSubjectCode",
                table: "SubjectSections",
                column: "SubjectsSubjectCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AEESubjects");

            migrationBuilder.DropTable(
                name: "CengSubjects");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "SubjectSections");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
