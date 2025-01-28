using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBenchmarkRepository
    {
        Task<IEnumerable<Benchmark>> GetBenchmarksAsync(bool trackChanges);
        Task<Benchmark> GetBenchmarkAsync(int benchmarkId, bool trackChanges);
        void CreateBenchmark(Benchmark benchmark);
    }
}
