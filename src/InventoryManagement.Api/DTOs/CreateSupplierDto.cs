using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.DTOs;

public class CreateSupplierDto
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ContactPerson { get; set; }

    [StringLength(30)]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }
}