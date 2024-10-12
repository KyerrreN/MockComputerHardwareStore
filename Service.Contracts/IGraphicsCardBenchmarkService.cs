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
        IEnumerable<GraphicsCardBenchmarkDto> GetBenchmarks(Guid grapicsCardId, bool trackChanges);
        GraphicsCardBenchmarkDto GetBenchmark(Guid graphicsCardId, int benchmarkId, bool trackChanges);
        GraphicsCardBenchmarkDto CreateGraphicsCardBenchmark(Guid graphicsCardId,
                                                             int benchmarkId,
                                                             GraphicsCardBenchmarkForCreationDto benchmark,
                                                             bool trackChanges);
        void DeleteBenchmarkForGraphicsCard(Guid graphicsCardId, int benchmarkId, bool trackChanges);
    }
}
