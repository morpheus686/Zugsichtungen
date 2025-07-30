using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class SightingProfile : Profile
    {
        public SightingProfile() 
        {
            CreateMap<SightingDto, Sighting>().ReverseMap();
        }
    }
}
