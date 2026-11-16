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

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Where(c => c.Id == id)
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
            .FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new { message = "Энэ ангилалд бараа холбогдсон байгаа тул устгах боломжгүй." });
        }

        return NoContent();
    }
}