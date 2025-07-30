using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDataService
    {
        Task<List<SightingViewEntryDto>> GetSichtungenAsync();
        Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync();
        Task<List<ContextDto>> GetKontextesAsync();
        Task AddSichtungAsync(SightingDto newSichtung);
        Task SaveChangesAsync();
    }
}
