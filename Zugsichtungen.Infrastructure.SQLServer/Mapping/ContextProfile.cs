using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Mapping
{
    public class ContextProfile : Profile
    {
        public ContextProfile()
        {
            CreateMap<ContextDto, Context>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Description));
        }
    }
}
