using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDataService
    {
        Task SaveChangesAsync();
        Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);
        Task<bool> DeleteSightingAsync(int sightingId);

        // Ab hier DDD-Methoden
        Task AddAsync(Sighting sighting);
        Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync();
        Task<List<Context>> GetContextesAsync();
        Task<List<VehicleViewEntry>> GetVehiclesAsync();
        Task<SightingPicture?> GetPictureBySightingIdAsync(int sightingId);
    }
}
