using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<GraphicsCard, GraphicsCardDto>()
                .ForCtorParam("FullName",
                opt => opt.MapFrom(x => string.Join(' ', x.Distributor, x.Manufacturer, x.Model)));

            CreateMap<GraphicsCardBenchmark, BenchmarkDto>()
                .ForCtorParam("Id",
                opt => opt.MapFrom(x => x.BenchmarkId))
                .ForCtorParam("GraphicsCardName",
                opt => opt.MapFrom(x => string.Join(' ', x.GraphicsCard.Distributor, x.GraphicsCard.Manufacturer, x.GraphicsCard.Model)))
                .ForCtorParam("GameName",
                opt => opt.MapFrom(x => x.Benchmark.GameName))
                .ForCtorParam("Resolution",
                opt => opt.MapFrom(x => x.Benchmark.Resolution.ToString()))
                .ForCtorParam("Settings",
                opt => opt.MapFrom(x => x.Benchmark.Settings.ToString()));
        }
    }
}
