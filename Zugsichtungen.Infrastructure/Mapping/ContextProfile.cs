using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class ContextProfile : Profile
    {
        public ContextProfile()
        {
            CreateMap<ContextDto, Context>().ReverseMap();
        }
    }
}
