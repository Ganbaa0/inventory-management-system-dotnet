namespace InventoryManagement.Api.DTOs
{
    public class ProductSummaryDto
    {
        public int Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public bool IsActive { get; set; }
    }
}