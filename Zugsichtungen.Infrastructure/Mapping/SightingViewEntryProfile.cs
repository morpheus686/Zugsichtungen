using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models.Sighting;

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
