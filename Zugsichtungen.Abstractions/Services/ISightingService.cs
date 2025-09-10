using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISightingService
    {
        Task UpdateContextes(List<Context> contextes);        
        Task<int> AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture);
        Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync();
        Task<List<ContextDto>> GetContextsAsync();
        Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync();
        Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId);
        Task<SightingViewEntryDto> GetSightingViewEntryBySightingIdAsync(int sightingId);
    }
}
