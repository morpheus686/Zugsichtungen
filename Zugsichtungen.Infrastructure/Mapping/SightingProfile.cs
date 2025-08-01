using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class SightingProfile : Profile
    {
        public SightingProfile() 
        {
            CreateMap<SightingDto, Sighting>().ReverseMap();

            CreateMap<SightingDto, Models.Sichtungen>()
                .ForMember(dest => dest.FahrzeugId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.KontextId, opt => opt.MapFrom(src => src.ContextId))
                .ForMember(dest => dest.Ort, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Bemerkung, opt => opt.MapFrom(src => src.Note));

            CreateMap<SightingDto, SqlServerModels.Sighting>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.ContextId, opt => opt.MapFrom(src => src.ContextId))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Note));
        }
    }
}
