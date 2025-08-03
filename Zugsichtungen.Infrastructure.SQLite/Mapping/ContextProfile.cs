using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Mapping
{
    public class ContextProfile : Profile
    {
        public ContextProfile()
        {
            CreateMap<ContextDto, Kontexte>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
