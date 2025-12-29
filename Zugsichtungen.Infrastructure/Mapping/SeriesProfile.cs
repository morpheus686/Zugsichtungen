using AutoMapper;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Series, SeriesDto>();
        }
    }
}
