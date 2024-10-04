using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        // CTOR
        public RepositoryContext(DbContextOptions options) : base (options)
        { 
        }

        // Db Sets
        public DbSet<Product>? Products { get; set; }
        public DbSet<GraphicsCard>? GraphicsCards { get; set; }
        public DbSet<Category>? Categories { get; set; }
    }
}
