using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IGalleryService
    {
        Task<List<PictureDto>> GetGalleryPicturesAsync();
        Task<ThumbnailDataDto?> GetThumbnailDataAsync(int pictureId);
        Task<GalleryPictureDataDto?> GetGalleryPictureDataDtoAsync(int pictureId);
    }
}
