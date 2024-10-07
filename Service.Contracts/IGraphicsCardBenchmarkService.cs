using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IGraphicsCardBenchmarkService
    {
        IEnumerable<BenchmarkDto> GetBenchmarks(Guid grapicsCardId, bool trackChanges);
        BenchmarkDto GetBenchmark(Guid graphicsCardId, int benchmarkId, bool trackChanges);
    }
}
