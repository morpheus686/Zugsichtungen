using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.SqlServerModels;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SqlServerDataService(TrainspottingContext context, IMapper mapper) : IDataService
    {
        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            Debug.WriteLine("Affected rows: " + affected);
        }

        public async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            var vehicleList = await context.Vehiclelists.ToListAsync();
            return [.. vehicleList.Select(mapper.Map<VehicleViewEntryDto>)];
        }

        public async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            var sichtungen = await context.SightingLists.ToListAsync();
            return [.. sichtungen.Select(ToDto)];
        }

        public async Task<List<ContextDto>> GetKontextesAsync()
        {
            var kontexte = await context.Contexts.ToListAsync();
            return [.. kontexte.Select(mapper.Map<ContextDto>)];
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

        public async Task UpdateContext(ContextDto dto, UpdateMode updateMode)
        {
            switch (updateMode)
            {
                case UpdateMode.Full:
                    await UpdateContextFullAsync(dto);
                    break;

                case UpdateMode.Partial:
                    await UpdateContextPartialAsync(dto);
                    break;

                case UpdateMode.Tracked:
                    await UpdateContextTrackedAsync(dto);
                    break;
            }
        }
        private async Task UpdateContextFullAsync(ContextDto dto)
        {
            var entity = new Context
            {
                Id = dto.Id,
                Description = dto.Name
            };

            context.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        private async Task UpdateContextTrackedAsync(ContextDto dto)
        {
            var entity = new Context { Id = dto.Id };

            context.Attach(entity);

            context.Entry(entity).Property(x => x.Description).CurrentValue = dto.Name;
            context.Entry(entity).Property(x => x.Description).IsModified = true;

            await context.SaveChangesAsync();
        }

        private async Task UpdateContextPartialAsync(ContextDto dto)
        {
            var entity = await context.Contexts.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity is null) return;

            // Mappen nur der Felder, die sich ändern dürfen
            entity.Description = dto.Name;

            await context.SaveChangesAsync();
        }
    }
}
