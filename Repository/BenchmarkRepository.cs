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

        public IEnumerable<Benchmark> GetBenchmarks(int benchmarkId, bool trackChanges)
        {
            return FindByCondition(b => b.Id.Equals(benchmarkId), trackChanges)
                .ToList();
        }
    }
}
