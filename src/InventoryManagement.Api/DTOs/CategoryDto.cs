namespace InventoryManagement.Api.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<ProductSummaryDto> Products { get; set; } = new List<ProductSummaryDto>();
    }
}