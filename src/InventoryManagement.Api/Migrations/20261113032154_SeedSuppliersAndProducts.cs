using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagement.Api.Migrations;

/// <inheritdoc />
public partial class _20261113032154_SeedSuppliersAndProducts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Suppliers",
            columns: new[] { "Id", "Address", "ContactPerson", "Email", "Name", "Phone" },
            values: new object[] { 1, null, "John Doe", "johndoe@example.com", "Supplier A", "123-456-7890" });

        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "CategoryId", "Description", "IsActive", "Name", "Price", "QuantityInStock", "ReorderLevel", "Sku", "SupplierId" },
            values: new object[,]
            {
                { 1, 1, "A high-end smartphone with advanced features.", true, "Smartphone", 699.99m, 50, 10, "ELEC001", 1 },
                { 2, 2, "A comfortable cotton t-shirt.", true, "T-Shirt", 19.99m, 100, 20, "CLOTH001", 1 },
                { 3, 3, "A captivating fiction novel.", true, "Novel", 14.99m, 30, 5, "BOOK001", 1 }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Suppliers",
            keyColumn: "Id",
            keyValue: 1);
    }
}
