using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Infrastructure.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly ISightingDataService dataService;
        private readonly IImageRepository imageRepository;

        public GalleryService(ISightingDataService dataService, IImageRepository imageRepository)
        {
            this.dataService = dataService;
            this.imageRepository = imageRepository;
        }
    }
}
