using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GraphicsCardBenchmarkRepository : RepositoryBase<GraphicsCardBenchmark>, IGraphicsCardBenchmarkRepository
    {
        public GraphicsCardBenchmarkRepository(RepositoryContext context)
            : base(context)
        {
            
        }

        public IEnumerable<GraphicsCardBenchmark> GetBenchmarks(Guid graphicsCardId, bool trackChanges)
        {
            return FindByCondition(gb => gb.GraphicsCardId.Equals(graphicsCardId), trackChanges)
                .Include(gb => gb.GraphicsCard)
                .Include(gb => gb.Benchmark)
                .OrderBy(gb => gb.Fps)
                .ToList();
        }
    }
}
