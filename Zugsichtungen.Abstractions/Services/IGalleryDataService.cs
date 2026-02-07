using Zugsichtungen.Domain.Models.Gallery;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IGalleryDataService
    {
        Task<List<Picture>> GetPicturesAsync();
        Task<ThumbnailData?> GetThumbnailByIdAsync(int id);
        Task<PictureData?> GetPictureByIdAsync(int id);
    }
}
