using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class GraphicsCardBenchmarkConfiguration : IEntityTypeConfiguration<GraphicsCardBenchmark>
    {
        public void Configure(EntityTypeBuilder<GraphicsCardBenchmark> builder)
        {
            builder.HasData
            (
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = 1,
                    Fps = 96.7M,
                    TestingTool = "Fraps"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = 2,
                    Fps = 112.3M,
                    TestingTool = "MSI Afterburner"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = 3,
                    Fps = 164.2M,
                    TestingTool = "RivaTuner Statistics Server"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = 4,
                    Fps = 24.3M,
                    TestingTool = "Shadowplay"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = 5,
                    Fps = 48.5M,
                    TestingTool = "MSI Afterburner"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = 6,
                    Fps = 76.1m,
                    TestingTool = "RivaTuner Statistics Server"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = 7,
                    Fps = 63.4m,
                    TestingTool = "RivaTuner Statistics Server"
                }
            );

            builder.HasKey(i => new { i.GraphicsCardId, i.BenchmarkId });
        }
    }
}
