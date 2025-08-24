using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISightingService
    {
        Task UpdateContextes(List<Context> contextes);        
        Task AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture);
        Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync();
        Task<List<ContextDto>> GetContextesAsync();
        Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync();
        Task<SightingPictureDto?> GetPictureBySightingIdAsync(int sightingId);
    }
}
