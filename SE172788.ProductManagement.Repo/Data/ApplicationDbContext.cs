using Microsoft.EntityFrameworkCore;
using SE172788.ProductManagement.Repo.Models;

namespace SE172788.ProductManagement.Repo.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}
