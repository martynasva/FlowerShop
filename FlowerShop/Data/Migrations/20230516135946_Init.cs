using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MerchandiseCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCategoryID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchandiseCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MerchandiseCategories_MerchandiseCategories_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "MerchandiseCategories",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Merchandises",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchandises", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionName = table.Column<string>(type: "text", nullable: false),
                    PermissionDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MerchandiseMerchandiseCategory",
                columns: table => new
                {
                    CategoriesID = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchandisesID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchandiseMerchandiseCategory", x => new { x.CategoriesID, x.MerchandisesID });
                    table.ForeignKey(
                        name: "FK_MerchandiseMerchandiseCategory_MerchandiseCategories_Catego~",
                        column: x => x.CategoriesID,
                        principalTable: "MerchandiseCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MerchandiseMerchandiseCategory_Merchandises_MerchandisesID",
                        column: x => x.MerchandisesID,
                        principalTable: "Merchandises",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserPermissionsID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserTypes_UserPermissions_UserPermissionsID",
                        column: x => x.UserPermissionsID,
                        principalTable: "UserPermissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    UserTypeID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeID",
                        column: x => x.UserTypeID,
                        principalTable: "UserTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderID = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryType = table.Column<string>(type: "text", nullable: true),
                    DeliveryOptions = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Deliveries_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchandiceID = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchandiseID = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderID = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "text", nullable: true),
                    DateOfManufacture = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Items_Merchandises_MerchandiseID",
                        column: x => x.MerchandiseID,
                        principalTable: "Merchandises",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_OrderID",
                table: "Deliveries",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_MerchandiseID",
                table: "Items",
                column: "MerchandiseID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderID",
                table: "Items",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_MerchandiseCategories_ParentCategoryID",
                table: "MerchandiseCategories",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MerchandiseMerchandiseCategory_MerchandisesID",
                table: "MerchandiseMerchandiseCategory",
                column: "MerchandisesID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeID",
                table: "Users",
                column: "UserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_UserPermissionsID",
                table: "UserTypes",
                column: "UserPermissionsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MerchandiseMerchandiseCategory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "MerchandiseCategories");

            migrationBuilder.DropTable(
                name: "Merchandises");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "UserPermissions");
        }
    }
}
