using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISightingService
    {
        Task AddSichtungAsync(DateOnly date, int vehicleId, int kontextId, string place, string? note, string? filePath);
        Task<List<SightingViewEntry>> GetAllSightingsAsync();
        Task<List<Context>> GetAllContextesAsync();
        Task UpdateContextes(List<Context> contextes);
        Task<List<VehicleViewEntry>> GetAllVehicleViewEntriesAsync();

    }
}
