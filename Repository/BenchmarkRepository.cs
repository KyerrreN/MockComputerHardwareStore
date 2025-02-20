using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BenchmarkRepository : RepositoryBase<Benchmark>, IBenchmarkRepository
    {
        public BenchmarkRepository(RepositoryContext context) 
            : base (context)
        {
        }
        public async Task<IEnumerable<Benchmark>> GetBenchmarksAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }
        public async Task<Benchmark> GetBenchmarkAsync(Guid benchmarkId, bool trackChanges)
        {
            return await FindByCondition(g => g.Id.Equals(benchmarkId), trackChanges)
                .SingleOrDefaultAsync();
        }
        public void CreateBenchmark(Benchmark benchmark)
        {
            Create(benchmark);
        }
    }
}
