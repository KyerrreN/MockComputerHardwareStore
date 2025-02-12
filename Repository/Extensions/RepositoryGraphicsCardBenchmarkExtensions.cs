using Entities.Models;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

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
        public static IQueryable<GraphicsCardBenchmark> Sort(this IQueryable<GraphicsCardBenchmark> benchmarks, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return benchmarks.OrderBy(x => x.BenchmarkId);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(GraphicsCardBenchmark).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(prop =>
                    prop.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrEmpty(orderQuery))
            {
                return benchmarks.OrderBy(x => x.BenchmarkId);
            }

            return benchmarks.OrderBy(orderQuery);
        }
    }
}
