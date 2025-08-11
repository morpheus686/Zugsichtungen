using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDataService
    {
        Task<List<SightingViewEntryDto>> GetSichtungenAsync();
        Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync();
        Task<List<ContextDto>> GetKontextesAsync();
        Task AddSichtungAsync(SightingDto newSichtung, SightingPictureDto? sightingPictureDto);
        Task SaveChangesAsync();
        Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);
        Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId);
        Task<bool> CheckIfSightingPictureExists(int sightingId);
    }
}
