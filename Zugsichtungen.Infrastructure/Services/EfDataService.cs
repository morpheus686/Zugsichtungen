using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Extensions;
using Zugsichtungen.Infrastructure.Models;

namespace Zugsichtungen.Infrastructure.Services
{
    public class EfDataService(ZugbeobachtungenContext context) : IDataService
    {
        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            Debug.WriteLine("Affected rows: " + affected);
        }

        public async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            var fahrzeuge = await context.Fahrzeuglistes.ToListAsync();
            return [.. fahrzeuge.Select(x => x.ToDto())];
        }

        public async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            var sichtungen = await context.Sichtungsviews.ToListAsync();
            return [.. sichtungen.Select(x => x.ToDto())];
        }

        public async Task<List<ContextDto>> GetKontextesAsync()
        {
            var kontexte = await context.Kontextes.ToListAsync();
            return [.. kontexte.Select(x => x.ToDto())];
        }

        public async Task AddSichtungAsync(SightingDto newSichtung)
        {
            await context.Sichtungens.AddAsync(newSichtung.FromDto());
        }
    }
}
