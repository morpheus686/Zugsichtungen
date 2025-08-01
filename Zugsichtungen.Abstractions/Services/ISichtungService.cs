using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISichtungService
    {
        Task AddSichtungAsync(DateOnly date, int vehicleId, int kontextId, string place, string? note);
        Task AddSichtungAsync(SightingDto sightingDto);
        Task<List<SightingViewEntry>> GetAllSightingsAsync();
        Task<List<Context>> GetAllContextesAsync();
        Task UpdateContextes(List<Context> contextes);
        Task<List<VehicleViewEntry>> GetAllVehicleViewEntriesAsync();

    }
}
