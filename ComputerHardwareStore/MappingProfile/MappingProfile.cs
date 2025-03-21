﻿using AutoMapper;
using Entities.Enum;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // GraphicsCard
            CreateMap<GraphicsCard, GraphicsCardDto>()
                .ForMember(g => g.FullName,
                opt => opt.MapFrom(x => string.Join(' ', x.Distributor, x.Manufacturer, x.Model)));

            CreateMap<GraphicsCardForCreationDto, GraphicsCard>();

            CreateMap<GraphicsCardForUpdateDto, GraphicsCard>();

            // GraphicsCardBenchmark
            CreateMap<GraphicsCardBenchmark, GraphicsCardBenchmarkDto>()
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

            CreateMap<GraphicsCardBenchmarkForCreationDto, GraphicsCardBenchmark>();

            CreateMap<GraphicsCardBenchmarkForUpdateDto, GraphicsCardBenchmark>().ReverseMap();

            // Benchmark
            CreateMap<Benchmark, BenchmarkDto>()
                .ForMember(b => b.Resolution,
                opt => opt.MapFrom(x => x.Resolution.ToString()))
                .ForMember(b => b.Settings,
                opt => opt.MapFrom(x => x.Settings.ToString()));

            CreateMap<BenchmarkForCreationDto, Benchmark>()
                .ForMember(x => x.Settings,
                opt => opt.MapFrom(x => (BenchmarkSettings)x.Settings))
                .ForMember(x => x.Resolution,
                opt => opt.MapFrom(x => (BenchmarkResolution)x.Resolution));

            // User
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
