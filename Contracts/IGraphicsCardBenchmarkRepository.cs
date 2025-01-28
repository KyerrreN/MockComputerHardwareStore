using Entities.Models;

namespace Contracts
{
    public interface IGraphicsCardBenchmarkRepository
    {
        Task<IEnumerable<GraphicsCardBenchmark>> GetBenchmarksAsync(Guid graphicsCardId, bool trackChanges);
        Task<GraphicsCardBenchmark> GetBenchmarkAsync(Guid graphicsCardId, int benchmarkId, bool trackChanges);
        void CreateGraphicsCardBenchmark(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmark benchmark);
        void DeleteGraphicsCardBenchmark(GraphicsCardBenchmark benchmark);
    }
}
