using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        // CTOR
        public RepositoryContext(DbContextOptions options) : base (options)
        { 
        }

        // OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new GraphicsCardConfiguration());
        }


        // Db Sets
        public DbSet<Product>? Products { get; set; }
        public DbSet<GraphicsCard>? GraphicsCards { get; set; }
        public DbSet<Category>? Categories { get; set; }
    }
}
