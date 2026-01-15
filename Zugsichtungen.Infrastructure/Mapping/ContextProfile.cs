using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models.Sighting;

namespace Zugsichtungen.Infrastructure.Mapping
{
    internal class ContextProfile : Profile
    {
        public ContextProfile()
        {
            CreateMap<Context, ContextDto>();
        }
    }
}
