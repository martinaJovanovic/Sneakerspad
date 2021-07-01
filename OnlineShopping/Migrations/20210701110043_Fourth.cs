using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "ShoppingCart",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShoppingCart",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "ShoppingCart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShoppingCart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
