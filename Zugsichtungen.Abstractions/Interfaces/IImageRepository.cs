using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Interfaces
{
    public interface IImageRepository
    {
        Task<SightingPicture?> GetImageBySightingIdAsync(int sightingId);
        Task<bool> CheckIfImageExistsAsync(int sightingId);
    }
}
