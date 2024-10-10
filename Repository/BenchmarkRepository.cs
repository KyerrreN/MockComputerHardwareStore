using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BenchmarkRepository : RepositoryBase<Benchmark>, IBenchmarkRepository
    {
        public BenchmarkRepository(RepositoryContext context) 
            : base (context)
        {
        }
        public IEnumerable<Benchmark> GetBenchmarks(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(b => b.Id)
                .ToList();
        }
        public Benchmark GetBenchmark(int benchmarkId, bool trackChanges)
        {
            return FindByCondition(g => g.Id.Equals(benchmarkId), trackChanges)
                .SingleOrDefault();
        }
        public void CreateBenchmark(Benchmark benchmark)
        {
            Create(benchmark);
        }
    }
}
