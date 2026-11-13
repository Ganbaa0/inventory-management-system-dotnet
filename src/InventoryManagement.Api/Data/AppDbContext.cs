using InventoryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets" },
            new Category { Id = 2, Name = "Clothing", Description = "Apparel and clothing items" },
            new Category { Id = 3, Name = "Books", Description = "Books and literature" }
        );
        modelBuilder.Entity<Supplier>().HasData(
            new Supplier
            {
                Id = 1,
                Name = "Supplier A",
                ContactPerson = "John Doe",
                Phone = "123-456-7890",
                Email = "johndoe@example.com"
            }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Sku = "ELEC001",
                Name = "Smartphone",
                Description = "A high-end smartphone with advanced features.",
                Price = 699.99m,
                QuantityInStock = 50,
                ReorderLevel = 10,
                IsActive = true,
                CategoryId = 1, // Electronics
                SupplierId = 1 // Supplier A
            },
            new Product
            {
                Id = 2,
                Sku = "CLOTH001",
                Name = "T-Shirt",
                Description = "A comfortable cotton t-shirt.",
                Price = 19.99m,
                QuantityInStock = 100,
                ReorderLevel = 20,
                IsActive = true,
                CategoryId = 2, // Clothing
                SupplierId = 1 // Supplier A
            },
            new Product
            {
                Id = 3,
                Sku = "BOOK001",
                Name = "Novel",
                Description = "A captivating fiction novel.",
                Price = 14.99m,
                QuantityInStock = 30,
                ReorderLevel = 5,
                IsActive = true,
                CategoryId = 3, // Books
                SupplierId = 1 // Supplier A
            }
        );
    }
}
