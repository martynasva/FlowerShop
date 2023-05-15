using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class MerchandiceManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchandiseCategories_MerchandiseCategories_ParentCategoryID",
                table: "MerchandiseCategories");

            migrationBuilder.DropIndex(
                name: "IX_MerchandiseCategories_ParentCategoryID",
                table: "MerchandiseCategories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryID",
                table: "MerchandiseCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "MerchandiseCategoryID",
                table: "MerchandiseCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MerchandiseCategories_MerchandiseCategoryID",
                table: "MerchandiseCategories",
                column: "MerchandiseCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_MerchandiseCategories_MerchandiseCategories_MerchandiseCate~",
                table: "MerchandiseCategories",
                column: "MerchandiseCategoryID",
                principalTable: "MerchandiseCategories",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchandiseCategories_MerchandiseCategories_MerchandiseCate~",
                table: "MerchandiseCategories");

            migrationBuilder.DropIndex(
                name: "IX_MerchandiseCategories_MerchandiseCategoryID",
                table: "MerchandiseCategories");

            migrationBuilder.DropColumn(
                name: "MerchandiseCategoryID",
                table: "MerchandiseCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryID",
                table: "MerchandiseCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MerchandiseCategories_ParentCategoryID",
                table: "MerchandiseCategories",
                column: "ParentCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_MerchandiseCategories_MerchandiseCategories_ParentCategoryID",
                table: "MerchandiseCategories",
                column: "ParentCategoryID",
                principalTable: "MerchandiseCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
