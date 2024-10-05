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
        }
    }
}
