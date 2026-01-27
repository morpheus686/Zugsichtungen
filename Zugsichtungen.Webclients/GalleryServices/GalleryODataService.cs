using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Webclients.GalleryServices
{
    public class GalleryODataService : IGalleryService
    {
        public Task<List<PictureDto>> GetGalleryPicturesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
