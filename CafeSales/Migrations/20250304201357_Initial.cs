using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeSales.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "order_statuses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment_types",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_order_statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "public",
                        principalTable: "order_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_payment_types_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalSchema: "public",
                        principalTable: "payment_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_products",
                schema: "public",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_products", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_order_products_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_products_products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "public",
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "order_statuses",
                column: "Id",
                values: new object[]
                {
                    new Guid("1f429036-0a5e-4888-a74f-cef8275f5df8"),
                    new Guid("51ed485a-d259-4b1e-9418-dd63147451ee"),
                    new Guid("76dfd036-cb2d-4a8a-ac50-1ed8a5908ba7")
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "payment_types",
                column: "Id",
                values: new object[]
                {
                    new Guid("16a1fec6-a9ea-40a9-b547-cb228401ccf9"),
                    new Guid("9fdbe7bf-68ca-4ef9-ac4a-aa8b78584a7f")
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_products_ProductId",
                schema: "public",
                table: "order_products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_PaymentTypeId",
                schema: "public",
                table: "orders",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_StatusId",
                schema: "public",
                table: "orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_products_Name",
                schema: "public",
                table: "products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_products",
                schema: "public");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "products",
                schema: "public");

            migrationBuilder.DropTable(
                name: "order_statuses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "payment_types",
                schema: "public");
        }
    }
}
