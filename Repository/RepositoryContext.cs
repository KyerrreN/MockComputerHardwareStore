using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        // CTOR
        public RepositoryContext(DbContextOptions options) : base (options)
        { 
        }

        // OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GraphicsCardConfiguration());
            modelBuilder.ApplyConfiguration(new BenchmarkConfiguration());
            modelBuilder.ApplyConfiguration(new GraphicsCardBenchmarkConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); 
        }

        // Db Sets
        public DbSet<GraphicsCard>? GraphicsCards { get; set; }
        public DbSet<Benchmark>? Benchmarks { get; set; }
        public DbSet<GraphicsCardBenchmark>? GraphicsCardBenchmarks { get; set; }
    }
}
