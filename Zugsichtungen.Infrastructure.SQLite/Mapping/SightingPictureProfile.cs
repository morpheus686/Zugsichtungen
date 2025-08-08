using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Mapping
{
    public class SightingPictureProfile : Profile
    {
        public SightingPictureProfile()
        {
            CreateMap<SightingPictureDto, SichtungBild>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Bild, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Dateiname, opt => opt.MapFrom(src => src.Filename))
                .ForMember(dest => dest.SichtungId, opt => opt.MapFrom(src => src.SightingId))
                .ReverseMap();
        }
    }
}
