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
            modelBuilder.ApplyConfiguration(new GraphicsCardConfiguration());
            modelBuilder.ApplyConfiguration(new BenchmarkConfiguration());
            modelBuilder.ApplyConfiguration(new GraphicsCardBenchmarkConfiguration());
        }


        // Db Sets
        public DbSet<GraphicsCard>? GraphicsCards { get; set; }
        public DbSet<Benchmark>? Benchmarks { get; set; }
        public DbSet<GraphicsCardBenchmark>? GraphicsCardBenchmarks { get; set; }
    }
}
