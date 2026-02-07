using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Foundation.Mapping;

namespace Zugsichtungen.Infrastructure.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryDataService dataService;
        private readonly IMapper mapper;

        public GalleryService(IGalleryDataService dataService, IMapper mapper)
        {
            this.dataService = dataService;
            this.mapper = mapper;
        }

        public async Task<GalleryPictureDataDto?> GetGalleryPictureDataDtoAsync(int pictureId)
        {
            var pictureData = await dataService.GetPictureByIdAsync(pictureId);

            if (pictureData is null)
            {
                return null;
            }

            return mapper.Map<PictureData, GalleryPictureDataDto>(pictureData);
        }

        public async Task<List<PictureDto>> GetGalleryPicturesAsync()
        {
            var pictures = await dataService.GetPicturesAsync();
            return mapper.MapList<Picture, PictureDto>(pictures);
        }

        public async Task<ThumbnailDataDto?> GetThumbnailDataAsync(int pictureId)
        {
            var thumbnailData = await dataService.GetThumbnailByIdAsync(pictureId);

            if (thumbnailData is null)
            {
                return null;
            }

            return mapper.Map<ThumbnailData, ThumbnailDataDto>(thumbnailData);
        }
    }
}
