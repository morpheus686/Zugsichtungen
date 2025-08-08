using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Mapping
{
    public class SightingProfile : Profile
    {
        public SightingProfile()
        {
            CreateMap<SightingDto, Sighting>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.ContextId, opt => opt.MapFrom(src => src.ContextId))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Note));
        }
    }
}
