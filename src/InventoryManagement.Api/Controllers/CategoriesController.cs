using InventoryManagement.Api.Data;
using InventoryManagement.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        return await _context.Categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Products = c.Products
                    .Select(p => new ProductSummaryDto
                    {
                        Id = p.Id,
                        Sku = p.Sku,
                        Name = p.Name,
                        Price = p.Price,
                        QuantityInStock = p.QuantityInStock,
                        IsActive = p.IsActive
                    })
                    .ToList()
            })
            .ToListAsync();
    }
}