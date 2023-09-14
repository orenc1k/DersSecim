﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    /// <inheritdoc />
    public partial class isActiveCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Subjects",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Subjects");
        }
    }
}
