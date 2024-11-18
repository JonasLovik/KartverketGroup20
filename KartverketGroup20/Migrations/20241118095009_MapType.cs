using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketGroup20.Migrations
{
    /// <inheritdoc />
    public partial class MapType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapType",
                table: "Reports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapType",
                table: "Reports");
        }
    }
}
