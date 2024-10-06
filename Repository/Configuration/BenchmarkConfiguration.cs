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
                    Fps = 96.7M,
                    Resolution = Entities.Enum.BenchmarkResolution.UltraHD,
                    Settings = Entities.Enum.BenchmarkSettings.High,
                    GraphicsCardId = new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b")
                }
            );
        }
    }
}
