using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Api.Migrations;

/// <inheritdoc />
public partial class _20261112081245_ConfigureProductRules : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Products_Categories_CategoryId",
            table: "Products");

        migrationBuilder.DropForeignKey(
            name: "FK_Products_Suppliers_SupplierId",
            table: "Products");

        migrationBuilder.AddForeignKey(
            name: "FK_Products_Categories_CategoryId",
            table: "Products",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Products_Suppliers_SupplierId",
            table: "Products",
            column: "SupplierId",
            principalTable: "Suppliers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Products_Categories_CategoryId",
            table: "Products");

        migrationBuilder.DropForeignKey(
            name: "FK_Products_Suppliers_SupplierId",
            table: "Products");

        migrationBuilder.AddForeignKey(
            name: "FK_Products_Categories_CategoryId",
            table: "Products",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Products_Suppliers_SupplierId",
            table: "Products",
            column: "SupplierId",
            principalTable: "Suppliers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
