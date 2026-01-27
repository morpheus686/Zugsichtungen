using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models.Gallery;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class PictureProfile : Profile
    {
        public PictureProfile()
        {
            CreateMap<Picture, PictureDto>();
        }
    }
}
