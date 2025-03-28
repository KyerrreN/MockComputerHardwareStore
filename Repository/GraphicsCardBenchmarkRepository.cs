﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    public class GraphicsCardBenchmarkRepository : RepositoryBase<GraphicsCardBenchmark>, IGraphicsCardBenchmarkRepository
    {
        public GraphicsCardBenchmarkRepository(RepositoryContext context)
            : base(context)
        {
            
        }
        public async Task<PagedList<GraphicsCardBenchmark>> GetBenchmarksAsync(Guid graphicsCardId, GraphicsCardBenchmarkParameters parameters, bool trackChanges)
        {
            var benchmarks = await FindByCondition(gb => gb.GraphicsCardId.Equals(graphicsCardId)
                                                   //&& gb.Fps >= parameters.MinFps
                                                   //&& gb.Fps <= parameters.MaxFps
                                                   , trackChanges)
                .FilterGraphicsCardBenchmarks(parameters.MinFps, parameters.MaxFps)
                .Search(parameters.SearchTerm)
                .Include(gb => gb.GraphicsCard)
                .Include(gb => gb.Benchmark)
                .Sort(parameters.OrderBy)
                .ToListAsync();

            return PagedList<GraphicsCardBenchmark>
                .ToPagedList(benchmarks, parameters.PageNumber, parameters.PageSize);                
        }
        public async Task<GraphicsCardBenchmark> GetBenchmarkAsync(Guid graphicsCardId, Guid benchmarkId, bool trackChanges)
        {
            return await FindByCondition(gb => gb.GraphicsCardId.Equals(graphicsCardId) && gb.BenchmarkId.Equals(benchmarkId), trackChanges)
                .Include(gb => gb.Benchmark)
                .Include(gb => gb.GraphicsCard)
                .SingleOrDefaultAsync();
        }

        public void CreateGraphicsCardBenchmark(Guid graphicsCardId, Guid benchmarkId, GraphicsCardBenchmark benchmark)
        {
            benchmark.GraphicsCardId = graphicsCardId;
            benchmark.BenchmarkId = benchmarkId;
            Create(benchmark);
        }

        public void DeleteGraphicsCardBenchmark(GraphicsCardBenchmark benchmark)
        {
            Delete(benchmark);
        }
    }
}
