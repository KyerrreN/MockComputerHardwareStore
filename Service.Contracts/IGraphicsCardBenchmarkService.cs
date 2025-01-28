using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IGraphicsCardBenchmarkService
    {
        Task<IEnumerable<GraphicsCardBenchmarkDto>> GetBenchmarksAsync(Guid grapicsCardId, bool trackChanges);
        Task<GraphicsCardBenchmarkDto> GetBenchmarkAsync(Guid graphicsCardId, int benchmarkId, bool trackChanges);
        Task<GraphicsCardBenchmarkDto> CreateGraphicsCardBenchmarkAsync(Guid graphicsCardId,
                                                             int benchmarkId,
                                                             GraphicsCardBenchmarkForCreationDto benchmark,
                                                             bool trackChanges);
        Task DeleteBenchmarkForGraphicsCardAsync(Guid graphicsCardId, int benchmarkId, bool trackChanges);
        Task UpdateGraphicsCardBenchmarkAsync(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark, bool gdTrackChanges, bool bnTrackChanges);

        Task<(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity)>
            GetGraphicsCardBenchmarkForPatchAsync(Guid graphicsCardId, int benchmarkId, bool gcTrackChanges, bool benchTrackChanges);

        Task SaveChangesForPatchAsync(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity);
    }
}
