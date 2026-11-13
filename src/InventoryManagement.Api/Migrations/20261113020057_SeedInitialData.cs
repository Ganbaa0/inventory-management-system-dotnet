using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagement.Api.Migrations;

/// <inheritdoc />
public partial class _20261113020057_SeedInitialData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[,]
            {
                { 1, "Electronic devices and gadgets", "Electronics" },
                { 2, "Apparel and clothing items", "Clothing" },
                { 3, "Books and literature", "Books" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 3);
    }
}
