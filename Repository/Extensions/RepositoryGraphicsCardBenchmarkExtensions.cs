using Entities.Models;

namespace Repository.Extensions
{
    public static class RepositoryGraphicsCardBenchmarkExtensions
    {
        public static IQueryable<GraphicsCardBenchmark> FilterGraphicsCardBenchmarks(this IQueryable<GraphicsCardBenchmark> benchmarks, decimal minFps, decimal maxFps)
        {
            return benchmarks.Where(b => (b.Fps >= minFps && b.Fps <= maxFps));
        }
        public static IQueryable<GraphicsCardBenchmark> Search(this IQueryable<GraphicsCardBenchmark> benchmarks, string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return benchmarks;
            }
                
            var lowerCaseSearchQuery = searchQuery.Trim().ToLower();

            return benchmarks.Where(b => b.TestingTool.ToLower().Contains(lowerCaseSearchQuery));
        }
    }
}
