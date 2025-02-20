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
                    BenchmarkId = new Guid("1d519389-f0de-4eef-b3d1-1938df9700f4"),
                    Fps = 96.7M,
                    TestingTool = "Fraps"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = new Guid("88d38fd2-8b75-4bf2-ba67-d43a7329c7b5"),
                    Fps = 112.3M,
                    TestingTool = "MSI Afterburner"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = new Guid("27817726-bd13-4784-a3f3-0ab9b10d6190"),
                    Fps = 164.2M,
                    TestingTool = "RivaTuner Statistics Server"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = new Guid("72cedae8-f648-4a4e-826b-47a85aec960e"),
                    Fps = 24.3M,
                    TestingTool = "Shadowplay"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = new Guid("ffdaf3aa-0c42-499c-b63b-1cca50a36b97"),
                    Fps = 48.5M,
                    TestingTool = "MSI Afterburner"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = new Guid("4092a98e-efef-480d-9cb0-d72238b62a51"),
                    Fps = 76.1m,
                    TestingTool = "RivaTuner Statistics Server"
                },
                new GraphicsCardBenchmark
                {
                    GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    BenchmarkId = new Guid("0be78fd9-f27c-4e59-a6d6-c26ee4fa93e4"),
                    Fps = 63.4m,
                    TestingTool = "RivaTuner Statistics Server"
                }
            );

            builder.HasKey(i => new { i.GraphicsCardId, i.BenchmarkId });
        }
    }
}
