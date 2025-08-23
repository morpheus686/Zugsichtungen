using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class SightingViewEntryProfile : Profile
    {
        public SightingViewEntryProfile()
        {
            CreateMap<SightingViewEntry, SightingViewEntryDto>();
        }
    }
}
