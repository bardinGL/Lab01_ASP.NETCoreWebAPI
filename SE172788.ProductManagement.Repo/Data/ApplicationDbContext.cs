using Microsoft.EntityFrameworkCore;
using SE172788.ProductManagement.Repo.Models;

namespace SE172788.ProductManagement.Repo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Books" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Smartphone", CategoryId = 1, UnitsInStock = 100, UnitPrice = 299.99m },
                new Product { ProductId = 2, ProductName = "Laptop", CategoryId = 1, UnitsInStock = 50, UnitPrice = 899.99m },
                new Product { ProductId = 3, ProductName = "Novel", CategoryId = 2, UnitsInStock = 200, UnitPrice = 19.99m }
            );
        }
    }
}
