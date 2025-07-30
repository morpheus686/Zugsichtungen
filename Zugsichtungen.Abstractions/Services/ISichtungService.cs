using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Abstractions.Services
{
    public interface ISichtungService
    {
        Task AddSichtungAsync(DateOnly date, int? vehicleId, int? kontextId, string place, string? note);
        Task<List<SightingViewEntry>> GetAllSightingsAsync();
    }
}
