using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStoreWebAppMVC.Data.Migrations
{
    public partial class AddedToyCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toy_OrderItem_OrderItemId",
                table: "Toy");

            migrationBuilder.DropIndex(
                name: "IX_Toy_OrderItemId",
                table: "Toy");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Toy");

            migrationBuilder.DropColumn(
                name: "ItemCost",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "OrderItem",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Order",
                newName: "Total");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Toy",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ToyId",
                table: "OrderItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ToyId",
                table: "OrderItem",
                column: "ToyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Toy_ToyId",
                table: "OrderItem",
                column: "ToyId",
                principalTable: "Toy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Toy_ToyId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ToyId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Toy");

            migrationBuilder.DropColumn(
                name: "ToyId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItem",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Order",
                newName: "Cost");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Toy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemCost",
                table: "OrderItem",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Toy_OrderItemId",
                table: "Toy",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toy_OrderItem_OrderItemId",
                table: "Toy",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
