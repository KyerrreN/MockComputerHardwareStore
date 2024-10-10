using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBenchmarkService
    {
        IEnumerable<BenchmarkDto> GetAllBenchmarks(bool trackChanges);
        BenchmarkDto GetBenchmark(int id, bool trackChanges);
        BenchmarkDto CreateBenchmark(BenchmarkForCreationDto benchmark);
    }
}
