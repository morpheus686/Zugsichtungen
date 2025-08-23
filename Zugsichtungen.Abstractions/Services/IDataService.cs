using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDataService
    {
        Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync();
        Task<List<ContextDto>> GetKontextesAsync();
        Task SaveChangesAsync();
        Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);
        Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId);
        Task<bool> DeleteSightingAsync(int sightingId);
        Task<List<SightingPictureDto>> GetAllSightingPicturesAsync();

        // Ab hier DDD-Methoden
        Task AddAsync(Sighting sighting);
        Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync();
        Task<List<Context>> GetContextesAsync();
        Task<List<VehicleViewEntry>> GetVehiclesAsync();
    }
}
