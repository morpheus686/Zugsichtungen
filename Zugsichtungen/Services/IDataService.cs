using Zugsichtungen.Models;

namespace Zugsichtungen.Services
{
    public interface IDataService
    {
        Task<List<Sichtungsview>> GetSichtungenAsync();
        Task<List<Fahrzeugliste>> GetAllFahrzeugeAsync();
        Task<List<Kontexte>> GetKontextesAsync();
        Task AddSichtungAsync(Sichtungen newSichtung);
        Task SaveChangesAsync();
    }
}
