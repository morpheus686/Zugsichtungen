using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models.Gallery;

namespace Zugsichtungen.Infrastructure.Mapping
{
    public class ThumbnailDataProfile : Profile
    {
        public ThumbnailDataProfile()
        {
            CreateMap<ThumbnailData, ThumbnailDataDto>();
        }
    }
}
