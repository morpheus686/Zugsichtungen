using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Mapping
{
    public class VehicleViewEntryProfile : Profile
    {
        public VehicleViewEntryProfile()
        {
            CreateMap<Fahrzeugliste, VehicleViewEntryDto>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Fahrzeug))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => src.BaureiheId));
        }
    }
}
