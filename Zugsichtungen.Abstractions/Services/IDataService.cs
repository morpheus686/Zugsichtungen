using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDataService
    {
        Task<List<SightingViewEntryDto>> GetSichtungenAsync();
        Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync();
        Task<List<ContextDto>> GetKontextesAsync();
        Task SaveChangesAsync();
        Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);
        Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId);
        Task<bool> CheckIfSightingPictureExists(int sightingId);
        Task<bool> DeleteSightingAsync(int sightingId);
        Task<List<SightingPictureDto>> GetAllSightingPicturesAsync();
        Task AddAsync(Sighting sighting);
    }
}
