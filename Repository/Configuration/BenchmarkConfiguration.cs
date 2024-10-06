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
    public class BenchmarkConfiguration : IEntityTypeConfiguration<Benchmark>
    {
        public void Configure(EntityTypeBuilder<Benchmark> builder)
        {
            builder.HasData
            (
                new Benchmark
                {
                    Id = 1,
                    GameName = "Cyberpunk 2077",
                    Resolution = Entities.Enum.BenchmarkResolution.UltraHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    Id = 2,
                    GameName = "Cyberpunk 2077",
                    Resolution = Entities.Enum.BenchmarkResolution.QuadHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    Id = 3,
                    GameName = "Cyberpunk 2077",
                    Resolution = Entities.Enum.BenchmarkResolution.FullHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    Id = 4,
                    GameName = "Horizon: Zero Dawn",
                    Resolution = Entities.Enum.BenchmarkResolution.UltraHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    Id = 5,
                    GameName = "Horizon: Zero Dawn",
                    Resolution = Entities.Enum.BenchmarkResolution.QuadHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    Id = 6,
                    GameName = "Horizon: Zero Dawn",
                    Resolution = Entities.Enum.BenchmarkResolution.QuadHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                }
            );
        }
    }
}
