using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.Abstractions.Interfaces
{
    public interface IImageRepository
    {
        Task<SightingPictureDto?> GetImageBySightingIdAsync(int sightingId);
    }
}
