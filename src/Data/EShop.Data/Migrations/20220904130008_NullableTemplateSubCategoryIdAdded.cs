using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Data.Migrations
{
    public partial class NullableTemplateSubCategoryIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_TemplateSubCategories_SubCategoryId",
                table: "Templates");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Templates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_TemplateSubCategories_SubCategoryId",
                table: "Templates",
                column: "SubCategoryId",
                principalTable: "TemplateSubCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_TemplateSubCategories_SubCategoryId",
                table: "Templates");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Templates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_TemplateSubCategories_SubCategoryId",
                table: "Templates",
                column: "SubCategoryId",
                principalTable: "TemplateSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
