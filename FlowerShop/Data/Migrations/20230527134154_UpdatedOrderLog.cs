using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrderLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLogs_Orders_OrderID",
                table: "OrderLogs");

            migrationBuilder.DropIndex(
                name: "IX_OrderLogs_OrderID",
                table: "OrderLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderLogs_OrderID",
                table: "OrderLogs",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLogs_Orders_OrderID",
                table: "OrderLogs",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
