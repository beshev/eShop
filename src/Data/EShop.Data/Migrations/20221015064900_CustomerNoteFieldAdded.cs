#nullable disable

namespace EShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CustomerNoteFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerNote",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerNote",
                table: "OrderItems");
        }
    }
}
