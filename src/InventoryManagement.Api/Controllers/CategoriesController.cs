using InventoryManagement.Api.Data;
using InventoryManagement.Api.DTOs;
using InventoryManagement.Api.Models;
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

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryCreateDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var result = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryCreateDto dto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        category.Name = dto.Name;
        category.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
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