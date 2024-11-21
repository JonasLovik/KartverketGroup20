using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketGroup20.Migrations
{
    /// <inheritdoc />
    public partial class Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Reports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reports");
        }
    }
}
