using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class ContextProfile : Profile
    {
        public ContextProfile()
        {
            CreateMap<ContextDto, Context>()
                .ReverseMap();

            CreateMap<ContextDto, Models.Kontexte>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<ContextDto, SqlServerModels.Context>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Description));
        }
    }
}
