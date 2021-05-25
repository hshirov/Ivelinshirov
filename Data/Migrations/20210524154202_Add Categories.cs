using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Artworks");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Artworks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Paintings" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Drawings" });

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_CategoryId",
                table: "Artworks",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Categories_CategoryId",
                table: "Artworks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Categories_CategoryId",
                table: "Artworks");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Artworks_CategoryId",
                table: "Artworks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Artworks");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Artworks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
