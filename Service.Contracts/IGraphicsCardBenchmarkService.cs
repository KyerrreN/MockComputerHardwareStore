using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IGraphicsCardBenchmarkService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetBenchmarksAsync(Guid grapicsCardId, 
                                                                       LinkParameters parameters, 
                                                                       bool trackChanges);
        Task<GraphicsCardBenchmarkDto> GetBenchmarkAsync(Guid graphicsCardId, Guid benchmarkId, bool trackChanges);
        Task<GraphicsCardBenchmarkDto> CreateGraphicsCardBenchmarkAsync(Guid graphicsCardId,
                                                             Guid benchmarkId,
                                                             GraphicsCardBenchmarkForCreationDto benchmark,
                                                             bool trackChanges);
        Task DeleteBenchmarkForGraphicsCardAsync(Guid graphicsCardId, Guid benchmarkId, bool trackChanges);
        Task UpdateGraphicsCardBenchmarkAsync(Guid graphicsCardId, Guid benchmarkId, GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmark, bool gdTrackChanges, bool bnTrackChanges);

        Task<(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity)>
            GetGraphicsCardBenchmarkForPatchAsync(Guid graphicsCardId, Guid benchmarkId, bool gcTrackChanges, bool benchTrackChanges);

        Task SaveChangesForPatchAsync(GraphicsCardBenchmarkForUpdateDto graphicsCardBenchmarkToPatch, GraphicsCardBenchmark graphicsCardBenchmarkEntity);
    }
}
