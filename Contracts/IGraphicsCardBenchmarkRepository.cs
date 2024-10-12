using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGraphicsCardBenchmarkRepository
    {
        IEnumerable<GraphicsCardBenchmark> GetBenchmarks(Guid graphicsCardId, bool trackChanges);
        GraphicsCardBenchmark GetBenchmark(Guid graphicsCardId, int benchmarkId, bool trackChanges);
        void CreateGraphicsCardBenchmark(Guid graphicsCardId, int benchmarkId, GraphicsCardBenchmark benchmark);
        void DeleteGraphicsCardBenchmark(GraphicsCardBenchmark benchmark);
    }
}
