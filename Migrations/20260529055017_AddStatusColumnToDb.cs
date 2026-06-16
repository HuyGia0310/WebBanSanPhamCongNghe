using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanSanPhamCongNghe.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusColumnToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updateAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    img = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__category__3213E83F698EF38F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contact",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__contact__3213E83F7AE3E31B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    img = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    registeredAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updateAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    randomKey = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    role = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__3213E83F6DCA3BD1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parentId = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    menuUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    menuIndex = table.Column<int>(type: "int", nullable: true),
                    isVisible = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__menu__3213E83F306B6A9A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    img = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    rate = table.Column<double>(type: "float", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updateAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    categoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product__3213E83F8259D521", x => x.id);
                    table.ForeignKey(
                        name: "FK__product__categor__4CA06362",
                        column: x => x.categoryId,
                        principalTable: "category",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<int>(type: "int", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    productId = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cart__3213E83FF564ABCE", x => x.id);
                    table.ForeignKey(
                        name: "FK__cart__customerId__47DBAE45",
                        column: x => x.customerId,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__cart__productId__48CFD27E",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rate = table.Column<int>(type: "int", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    productId = table.Column<int>(type: "int", nullable: true),
                    customerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_1", x => x.id);
                    table.ForeignKey(
                        name: "FK_review_customer",
                        column: x => x.customerId,
                        principalTable: "customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_review_product",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    total = table.Column<double>(type: "float", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đã đặt hàng"),
                    cartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payment__3213E83FDA28F8CC", x => x.id);
                    table.ForeignKey(
                        name: "FK_payment_cart",
                        column: x => x.cartId,
                        principalTable: "cart",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "paymentDetail",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false),
                    paymentId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<double>(type: "float", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_a", x => new { x.productId, x.paymentId });
                    table.ForeignKey(
                        name: "FK_paymentDetail_payment",
                        column: x => x.paymentId,
                        principalTable: "payment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_paymentDetail_product",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_customerId",
                table: "cart",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_cart_productId",
                table: "cart",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_cartId",
                table: "payment",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentDetail_paymentId",
                table: "paymentDetail",
                column: "paymentId");

            migrationBuilder.CreateIndex(
                name: "IX_product_categoryId",
                table: "product",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_review_customerId",
                table: "review",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_review_productId",
                table: "review",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "paymentDetail");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
