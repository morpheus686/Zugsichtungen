using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IGalleryService
    {
        Task<List<PictureDto>> GetGalleryPicturesAsync();
    }
}
