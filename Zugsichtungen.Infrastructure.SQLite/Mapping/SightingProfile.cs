using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Mapping
{
    public class SightingProfile : Profile
    {
        public SightingProfile()
        {
            CreateMap<SightingDto, Sichtungen>()
                .ForMember(dest => dest.FahrzeugId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.KontextId, opt => opt.MapFrom(src => src.ContextId))
                .ForMember(dest => dest.Ort, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Bemerkung, opt => opt.MapFrom(src => src.Note));
        }
    }
}
