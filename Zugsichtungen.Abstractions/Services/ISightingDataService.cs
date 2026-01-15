using Zugsichtungen.Domain.Models.Sighting;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISightingDataService
    {
        // Ab hier DDD-Methoden
        Task<int> AddAsync(Sighting sighting);
        Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync();
        Task<List<Context>> GetContextsAsync();
        Task<List<VehicleViewEntry>> GetVehicleViewEntriesAsync();
        Task<SightingPicture?> GetPictureBySightingIdAsync(int sightingId);
        Task<SightingViewEntry?> GetSightingViewEntryAsync(int sightingId);
        Task<List<Series>> GetAllSeriesAsync();
        Task<List<Vehicle>> GetAllVehiclesAsync();
    }
}
