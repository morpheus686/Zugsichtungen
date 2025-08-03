using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Mapping
{
    public class SightingViewEntryProfile : Profile
    {
        public SightingViewEntryProfile()
        {
            CreateMap<SightingList, SightingViewEntryDto>()
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.VehicleNumber))
                .ForMember(dest => dest.Context, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SightingDate))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Comment));
        }
    }
}
