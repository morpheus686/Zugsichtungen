using Zugsichtungen.Models;

namespace Zugsichtungen.Services
{
    public interface IDataService
    {
        Task<List<Sichtungen>> GetSichtungenAsync();
    }
}
