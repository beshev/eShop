using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInfos_Products_ProductId",
                table: "OrdersInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInfos_Templates_TemplateId",
                table: "OrdersInfos");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "OrdersInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrdersInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Base64",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInfos_Products_ProductId",
                table: "OrdersInfos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInfos_Templates_TemplateId",
                table: "OrdersInfos",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInfos_Products_ProductId",
                table: "OrdersInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersInfos_Templates_TemplateId",
                table: "OrdersInfos");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "OrdersInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrdersInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Base64",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInfos_Products_ProductId",
                table: "OrdersInfos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersInfos_Templates_TemplateId",
                table: "OrdersInfos",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
