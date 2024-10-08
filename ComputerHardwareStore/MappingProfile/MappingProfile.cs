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
                .ForMember(g => g.FullName,
                opt => opt.MapFrom(x => string.Join(' ', x.Distributor, x.Manufacturer, x.Model)));

            CreateMap<GraphicsCardBenchmark, BenchmarkDto>()
                .ForMember(g => g.Id,
                opt => opt.MapFrom(x => x.BenchmarkId))
                .ForMember(g => g.GraphicsCardName,
                opt => opt.MapFrom(x => string.Join(' ', x.GraphicsCard.Distributor, x.GraphicsCard.Manufacturer, x.GraphicsCard.Model)))
                .ForMember(g => g.GameName,
                opt => opt.MapFrom(x => x.Benchmark.GameName))
                .ForMember(g => g.Resolution,
                opt => opt.MapFrom(x => x.Benchmark.Resolution.ToString()))
                .ForMember(g => g.Settings,
                opt => opt.MapFrom(x => x.Benchmark.Settings.ToString()));
        }
    }
}
