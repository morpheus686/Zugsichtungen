using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Mapping
{
    public class VehicleViewEntryProfile : Profile
    {
        public VehicleViewEntryProfile()
        {
            CreateMap<Vehiclelist, VehicleViewEntryDto>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.VehicleDesignation));
        }
    }
}
