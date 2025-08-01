using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.SqlServerModels;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SqlServerDataService(TrainspottingContext context) : IDataService
    {
        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            Debug.WriteLine("Affected rows: " + affected);
        }

        public async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            var fahrzeuge = await context.Vehiclelists.ToListAsync();
            return [.. fahrzeuge.Select(ToDto)];
        }

        public async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            var sichtungen = await context.SightingLists.ToListAsync();
            return [.. sichtungen.Select(ToDto)];
        }

        public async Task<List<ContextDto>> GetKontextesAsync()
        {
            var kontexte = await context.Contexts.ToListAsync();
            return [.. kontexte.Select(ToDto)];
        }

        public async Task AddSichtungAsync(SightingDto newSichtung)
        {
            await context.Sightings.AddAsync(FromDto(newSichtung));
        }

        private static Sighting FromDto(SightingDto dto)
        {
            return new Sighting
            {
                ContextId = dto.ContextId,
                VehicleId = dto.VehicleId,
                Location = dto.Location,
                Date = dto.Date,
                Comment = dto.Note
            };
        }

        private static SightingViewEntryDto ToDto(SightingList entity)
        {
            return new SightingViewEntryDto
            {
                Id = entity.Id,
                VehicleNumber = entity.VehicleNumber,
                Location = entity.Location,
                Date = entity.SightingDate,
                Note = entity.Comment
            };
        }

        private static VehicleViewEntryDto ToDto(Vehiclelist entity)
        {
            return new VehicleViewEntryDto
            {
                Id = entity.Id,
                Vehicle = entity.VehicleDesignation
            };
        }

        private static ContextDto ToDto(Context entity)
        {
            return new ContextDto
            {
                Id = entity.Id,
                Name = entity.Description
            };
        }
    }
}
