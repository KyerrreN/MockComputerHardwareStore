using Entities.Models;
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
        void UpdateGraphicsCardBenchmark(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark, bool gdTrackChanges, bool bnTrackChanges);

        (GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity)
            GetGraphicsCardBenchmarkForPatch(Guid graphicsCardId, int benchmarkId, bool gcTrackChanges, bool benchTrackChanges);

        void SaveChangesForPatch(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity);
    }
}
