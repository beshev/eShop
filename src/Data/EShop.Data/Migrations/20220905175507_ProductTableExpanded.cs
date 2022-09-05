using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Data.Migrations
{
    public partial class ProductTableExpanded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagesFixedCount",
                table: "Templates",
                newName: "ImagesCount");

            migrationBuilder.AddColumn<bool>(
                name: "HasFontStyle",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ImagesCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFontStyle",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagesCount",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ImagesCount",
                table: "Templates",
                newName: "ImagesFixedCount");
        }
    }
}
