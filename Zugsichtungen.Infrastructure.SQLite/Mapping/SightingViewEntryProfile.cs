using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Mapping
{
    public class SightingViewEntryProfile : Profile
    {
        public SightingViewEntryProfile()
        {
            CreateMap<Sichtungsview, SightingViewEntryDto>()
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.Loknummer))
                .ForMember(dest => dest.Context, opt => opt.MapFrom(src => src.Thema))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Ort))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Datum))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Bemerkung))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Bild));

        }
    }
}
