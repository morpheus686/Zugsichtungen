using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class VehicleViewEntryProfile : Profile
    {
        public VehicleViewEntryProfile()
        {
            CreateMap<VehicleViewEntryDto, VehicleViewEntry>();

            CreateMap<Models.Fahrzeugliste, VehicleViewEntryDto>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Fahrzeug));

            CreateMap<SqlServerModels.Vehiclelist, VehicleViewEntryDto>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.VehicleDesignation));
        }
    }
}
