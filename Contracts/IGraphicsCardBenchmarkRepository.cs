using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IGraphicsCardBenchmarkRepository
    {
        Task<PagedList<GraphicsCardBenchmark>> GetBenchmarksAsync(Guid graphicsCardId,
                                                                    GraphicsCardBenchmarkParameters parameters,
                                                                    bool trackChanges);
        Task<GraphicsCardBenchmark> GetBenchmarkAsync(Guid graphicsCardId, Guid benchmarkId, bool trackChanges);
        void CreateGraphicsCardBenchmark(Guid graphicsCardId, Guid benchmarkId, GraphicsCardBenchmark benchmark);
        void DeleteGraphicsCardBenchmark(GraphicsCardBenchmark benchmark);
    }
}
