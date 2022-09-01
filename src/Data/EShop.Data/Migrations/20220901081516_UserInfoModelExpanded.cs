using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Data.Migrations
{
    public partial class UserInfoModelExpanded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "FirsName",
                table: "UsersInfo",
                newName: "FirstName");

            migrationBuilder.AddColumn<int>(
                name: "Carrier",
                table: "UsersInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                table: "UsersInfo",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressType",
                table: "UsersInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegisteredByVAT",
                table: "UsersInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressType",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "IsRegisteredByVAT",
                table: "UsersInfo");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "UsersInfo",
                newName: "FirsName");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
