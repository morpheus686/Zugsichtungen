using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Extensions;
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

        public async Task<List<VehicleViewEntry>> GetAllFahrzeugeAsync()
        {
            var fahrzeuge = await this.context.Fahrzeuglistes.ToListAsync();
            return [.. fahrzeuge.Select(x => x.ToDto())];
        }

        public async Task<List<SightingViewEntry>> GetSichtungenAsync()
        {
            var sichtungen = await this.context.Sichtungsviews.ToListAsync();
            return [.. sichtungen.Select(x => x.ToDto())];
        }

        public async Task<List<Context>> GetKontextesAsync()
        {
            var kontexte = await this.context.Kontextes.ToListAsync();
            return [.. kontexte.Select(x => x.ToDto())];
        }

        public async Task AddSichtungAsync(Sighting newSichtung)
        {
            await this.context.Sichtungens.AddAsync(newSichtung.FromDto());
        }
    }
}
