using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "ProductSize",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_ShoppingCartId",
                table: "ProductSize",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_ShoppingCart_ShoppingCartId",
                table: "ProductSize",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_ShoppingCart_ShoppingCartId",
                table: "ProductSize");

            migrationBuilder.DropIndex(
                name: "IX_ProductSize_ShoppingCartId",
                table: "ProductSize");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ProductSize");
        }
    }
}
