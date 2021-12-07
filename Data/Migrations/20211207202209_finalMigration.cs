using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStoreWebAppMVC.Data.Migrations
{
    public partial class finalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Toy_ToyId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ToyId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ToyId",
                table: "OrderItem");

            migrationBuilder.AddColumn<string>(
                name: "ToyColor",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ToyCost",
                table: "OrderItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ToyManufacturer",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToyName",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToyColor",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ToyCost",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ToyManufacturer",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ToyName",
                table: "OrderItem");

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
    }
}
