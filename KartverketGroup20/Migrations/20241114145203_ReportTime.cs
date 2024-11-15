using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketGroup20.Migrations
{
    /// <inheritdoc />
    public partial class ReportTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReportTime",
                table: "Reports",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportTime",
                table: "Reports");
        }
    }
}
