using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    /// <inheritdoc />
    public partial class PlaceInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstructorName",
                table: "SectionDays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "SectionDays",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorName",
                table: "SectionDays");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "SectionDays");
        }
    }
}
