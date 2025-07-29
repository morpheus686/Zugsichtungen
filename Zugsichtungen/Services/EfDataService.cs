using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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

        public async Task SaveChangesAsync()
        {
            var affected = await this.context.SaveChangesAsync();
            Debug.WriteLine(affected);
        }

        public Task<List<Fahrzeugliste>> GetAllFahrzeugeAsync()
        {
            var fahrzeuge = this.context.Fahrzeuglistes;
            return fahrzeuge.ToListAsync();
        }

        public Task<List<Sichtungsview>> GetSichtungenAsync()
        {
            var sichtungen = this.context.Sichtungsviews;
            return sichtungen.ToListAsync();
        }

        public Task<List<Kontexte>> GetKontextesAsync()
        {
            var kontexte = this.context.Kontextes;
            return kontexte.ToListAsync();
        }

        public async Task AddSichtungAsync(Sichtungen newSichtung)
        {
            await this.context.Sichtungens.AddAsync(newSichtung);
        }
    }
}
