using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class SightingPictureProfile : Profile
    {
        public SightingPictureProfile()
        {
            CreateMap<SightingPictureDto, SightingPicture>().ReverseMap();
        }
    }
}
