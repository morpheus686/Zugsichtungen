using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISightingService
    {
        Task<List<Context>> GetAllContextesAsync();
        Task UpdateContextes(List<Context> contextes);
        Task<List<VehicleViewEntry>> GetAllVehicleViewEntriesAsync();
        Task<SightingPicture?> GetSightingPictureByIdAsync(int id);
        Task<bool> CheckIfPictureExists(int sightingId);
        Task AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture);
        Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync();

    }
}
