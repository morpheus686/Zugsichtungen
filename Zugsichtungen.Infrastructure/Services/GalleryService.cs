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

        public async Task<List<PictureDto>> GetGalleryPicturesAsync()
        {
            var pictures = await dataService.GetPicturesAsync();
            return mapper.MapList<Picture, PictureDto>(pictures);
        }
    }
}
