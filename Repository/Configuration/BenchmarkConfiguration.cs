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
                    //Id = 1,
                    Id = new Guid("1d519389-f0de-4eef-b3d1-1938df9700f4"),
                    GameName = "Cyberpunk 2077",
                    Resolution = Entities.Enum.BenchmarkResolution.UltraHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    //Id = 2,
                    Id = new Guid("88d38fd2-8b75-4bf2-ba67-d43a7329c7b5"),
                    GameName = "Cyberpunk 2077",
                    Resolution = Entities.Enum.BenchmarkResolution.QuadHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    //Id = 3,
                    Id = new Guid("27817726-bd13-4784-a3f3-0ab9b10d6190"),
                    GameName = "Cyberpunk 2077",
                    Resolution = Entities.Enum.BenchmarkResolution.FullHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    //Id = 4,
                    Id = new Guid("72cedae8-f648-4a4e-826b-47a85aec960e"),
                    GameName = "Horizon: Zero Dawn",
                    Resolution = Entities.Enum.BenchmarkResolution.UltraHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    //Id = 5,
                    Id = new Guid("ffdaf3aa-0c42-499c-b63b-1cca50a36b97"),
                    GameName = "Horizon: Zero Dawn",
                    Resolution = Entities.Enum.BenchmarkResolution.QuadHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    //Id = 6,
                    Id = new Guid("4092a98e-efef-480d-9cb0-d72238b62a51"),
                    GameName = "Horizon: Zero Dawn",
                    Resolution = Entities.Enum.BenchmarkResolution.QuadHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                },
                new Benchmark
                {
                    //Id = 7,
                    Id = new Guid("0be78fd9-f27c-4e59-a6d6-c26ee4fa93e4"),
                    GameName = "Marvel Rivals",
                    Resolution = Entities.Enum.BenchmarkResolution.FullHD,
                    Settings = Entities.Enum.BenchmarkSettings.High
                }
            );
        }
    }
}
