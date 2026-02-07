
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Domain.Models.Sighting;

namespace Zugsichtungen.Abstractions.Interfaces
{
    public interface IImageRepository
    {
        Task<SightingPicture?> GetImageBySightingIdAsync(int sightingId);
        Task<bool> CheckIfImageExistsAsync(int sightingId);
        Task<Picture?> GetGalleryPictureByIdAsync(int imageId);
        Task<ThumbnailData?> GetThumbnailDataByIdAsync(int imageId);
        Task<PictureData?> GetPictureDataByIdAsync(int imageId);
    }
}
