using Microsoft.EntityFrameworkCore;
using Zugsichtungen.Models;

namespace Zugsichtungen.Services
{
    public class EfDataService : IDataService
    {
        private readonly ZugbeobachtungenContext context;

        public EfDataService(ZugbeobachtungenContext context)
        {
            this.context = context;
        }

        public Task<List<Sichtungen>> GetSichtungenAsync()
        {
            var sichtungen = this.context.Sichtungens
                .Include(e => e.Fahrzeug)
                .ThenInclude(e => e.Baureihe)
                .Include(e => e.Kontext)
                .OrderBy(e => e.Datum);

            return sichtungen.ToListAsync();
        }
    }
}
