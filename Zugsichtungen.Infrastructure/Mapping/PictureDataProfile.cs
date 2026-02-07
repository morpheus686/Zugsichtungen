using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models.Gallery;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class PictureDataProfile : Profile
    {
        public PictureDataProfile()
        {
            CreateMap<PictureData, GalleryPictureDataDto>();
        }
    }
}
