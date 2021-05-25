using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Addedpositionindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionPreference",
                table: "Artworks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionPreference",
                table: "Artworks");
        }
    }
}
