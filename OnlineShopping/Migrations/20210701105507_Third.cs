using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ShoppingCart",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "ShoppingCart",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_SizeId",
                table: "ShoppingCart",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Product_ProductId",
                table: "ShoppingCart",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Size_SizeId",
                table: "ShoppingCart",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Product_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Size_SizeId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_SizeId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "ShoppingCart");
        }
    }
}
