using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISightingService
    {        
        Task UpdateContextes(List<Context> contextes);
        [Obsolete("Use AddSightingAsync with SightingWithPictureDto parameter instead.")]
        Task<int> AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture);
        Task<int> AddSightingAsync(SightingWithPictureDto sightingWithPicture);
        Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync();
        Task<List<ContextDto>> GetContextsAsync();
        Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync();
        Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId);
        Task<SightingViewEntryDto> GetSightingViewEntryBySightingIdAsync(int sightingId);
    }
}
