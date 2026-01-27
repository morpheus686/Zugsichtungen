using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Webclients.GalleryServices
{
    public class GalleryApiService : IGalleryService
    {
        public Task<List<PictureDto>> GetGalleryPicturesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
