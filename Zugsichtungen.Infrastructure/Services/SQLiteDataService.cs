using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Models;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SQLiteDataService(ZugbeobachtungenContext context) : IDataService
    {
        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            Debug.WriteLine("Affected rows: " + affected);
        }

        public async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            var fahrzeuge = await context.Fahrzeuglistes.ToListAsync();
            return [.. fahrzeuge.Select(ToDto)];
        }

        public async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            var sichtungen = await context.Sichtungsviews.ToListAsync();
            return [.. sichtungen.Select(ToDto)];
        }

        public async Task<List<ContextDto>> GetKontextesAsync()
        {
            var kontexte = await context.Kontextes.ToListAsync();
            return [.. kontexte.Select(ToDto)];
        }

        public async Task AddSichtungAsync(SightingDto newSichtung)
        {
            await context.Sichtungens.AddAsync(FromDto(newSichtung));
        }

        private static SightingViewEntryDto ToDto(Sichtungsview entity)
        {
            return new SightingViewEntryDto
            {
                Id = entity.Id,
                VehicleNumber = entity.Loknummer,
                Location = entity.Ort,
                Date = entity.Datum,
                Note = entity.Bemerkung
            };
        }

        private static VehicleViewEntryDto ToDto(Fahrzeugliste entity)
        {
            return new VehicleViewEntryDto
            {
                Id = entity.Id,
                Vehicle = entity.Fahrzeug
            };
        }

        private static ContextDto ToDto(Kontexte entity)
        {
            return new ContextDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        private static Sichtungen FromDto(SightingDto dto)
        {
            return new Sichtungen
            {
                KontextId = dto.ContextId,
                FahrzeugId = dto.VehicleId,
                Ort = dto.Location,
                Datum = dto.Date,
                Bemerkung = dto.Note
            };
        }
    }
}
