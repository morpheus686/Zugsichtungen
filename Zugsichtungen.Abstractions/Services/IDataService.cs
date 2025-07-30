using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDataService
    {
        Task<List<SightingViewEntry>> GetSichtungenAsync();
        Task<List<VehicleViewEntry>> GetAllFahrzeugeAsync();
        Task<List<Context>> GetKontextesAsync();
        Task AddSichtungAsync(Sighting newSichtung);
        Task SaveChangesAsync();
    }
}
