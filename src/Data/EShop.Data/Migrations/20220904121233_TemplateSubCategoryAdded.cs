#nullable disable

namespace EShop.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class TemplateSubCategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TemplateCategoryId",
                table: "Templates",
                newName: "SubCategoryId");

            migrationBuilder.CreateTable(
                name: "TemplateSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateSubCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_SubCategoryId",
                table: "Templates",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_TemplateSubCategories_SubCategoryId",
                table: "Templates",
                column: "SubCategoryId",
                principalTable: "TemplateSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_TemplateSubCategories_SubCategoryId",
                table: "Templates");

            migrationBuilder.DropTable(
                name: "TemplateSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Templates_SubCategoryId",
                table: "Templates");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Templates",
                newName: "TemplateCategoryId");
        }
    }
}
