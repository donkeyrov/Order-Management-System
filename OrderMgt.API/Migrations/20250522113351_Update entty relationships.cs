using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderMgt.API.Migrations
{
    /// <inheritdoc />
    public partial class Updateenttyrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Segments_SegmentID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Segments_SegmentID",
                table: "Promotions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Customers_CustomerID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCodes_TransactionCodeID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionCodeID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_SegmentID",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Customers_SegmentID",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerID",
                table: "Transactions",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionCodeID",
                table: "Transactions",
                column: "TransactionCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_SegmentID",
                table: "Promotions",
                column: "SegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_SegmentID",
                table: "Customers",
                column: "SegmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Segments_SegmentID",
                table: "Customers",
                column: "SegmentID",
                principalTable: "Segments",
                principalColumn: "SegmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID",
                principalTable: "ProductCategories",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Segments_SegmentID",
                table: "Promotions",
                column: "SegmentID",
                principalTable: "Segments",
                principalColumn: "SegmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Customers_CustomerID",
                table: "Transactions",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCodes_TransactionCodeID",
                table: "Transactions",
                column: "TransactionCodeID",
                principalTable: "TransactionCodes",
                principalColumn: "TransactionCodeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
