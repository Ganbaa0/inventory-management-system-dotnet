using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.DTOs;

public class CreateProductDto
{
    [Required]
    [StringLength(50)]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Range(0, 1000000)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int QuantityInStock { get; set; }

    [Range(0, int.MaxValue)]
    public int ReorderLevel { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }
}