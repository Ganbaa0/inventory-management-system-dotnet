using InventoryManagement.Api.Data;
using InventoryManagement.Api.DTOs;
using InventoryManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        return await _context.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Sku = p.Sku,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                ReorderLevel = p.ReorderLevel,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category!.Name,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier!.Name
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Sku = p.Sku,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                ReorderLevel = p.ReorderLevel,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category!.Name,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier!.Name
            })
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto dto)
    {
        var skuExists = await _context.Products.AnyAsync(p => p.Sku == dto.Sku);

        if (skuExists)
        {
            return Conflict(new { message = "Ийм SKU-тай бараа аль хэдийн бүртгэгдсэн байна." });
        }

        var category = await _context.Categories.FindAsync(dto.CategoryId);

        if (category == null)
        {
            return BadRequest(new { message = "Ангилал олдсонгүй." });
        }

        var supplier = await _context.Suppliers.FindAsync(dto.SupplierId);

        if (supplier == null)
        {
            return BadRequest(new { message = "Нийлүүлэгч олдсонгүй." });
        }

        var product = new Product
        {
            Sku = dto.Sku,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            QuantityInStock = dto.QuantityInStock,
            ReorderLevel = dto.ReorderLevel,
            IsActive = true,
            CategoryId = category.Id,
            SupplierId = supplier.Id
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = new ProductDto
        {
            Id = product.Id,
            Sku = product.Sku,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock,
            ReorderLevel = product.ReorderLevel,
            IsActive = product.IsActive,
            CategoryId = category.Id,
            CategoryName = category.Name,
            SupplierId = supplier.Id,
            SupplierName = supplier.Name
        };

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, CreateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        // Өөрийн ID-аас бусад бараан дээр ижил SKU байвал зөрчил
        var skuTaken = await _context.Products.AnyAsync(p => p.Sku == dto.Sku && p.Id != id);

        if (skuTaken)
        {
            return Conflict(new { message = "Ийм SKU-тай бараа аль хэдийн бүртгэгдсэн байна." });
        }

        var category = await _context.Categories.FindAsync(dto.CategoryId);

        if (category == null)
        {
            return BadRequest(new { message = "Ангилал олдсонгүй." });
        }

        var supplier = await _context.Suppliers.FindAsync(dto.SupplierId);

        if (supplier == null)
        {
            return BadRequest(new { message = "Нийлүүлэгч олдсонгүй." });
        }

        product.Sku = dto.Sku;
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.QuantityInStock = dto.QuantityInStock;
        product.ReorderLevel = dto.ReorderLevel;
        product.CategoryId = category.Id;
        product.SupplierId = supplier.Id;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}