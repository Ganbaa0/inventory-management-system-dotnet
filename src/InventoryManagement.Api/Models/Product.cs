namespace InventoryManagement.Api.Models;

public class Product
{
    public int Id { get; set; }

    // SKU = Stock Keeping Unit, бараа бүрийн давтагдашгүй код
    public string Sku { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }

    public int ReorderLevel { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
}