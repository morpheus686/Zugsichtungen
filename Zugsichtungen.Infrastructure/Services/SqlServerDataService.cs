using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Infrastructure.SqlServerModels;
using Zugsichtungen.Foundation.Mapping;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SqlServerDataService : DataServiceBase
    {
        private readonly TrainspottingContext context;
        private readonly IMapper mapper;

        public SqlServerDataService(TrainspottingContext context, IMapper mapper) : base(context) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        public override async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            var vehicleList = await context.Vehiclelists.ToListAsync();
            return mapper.MapList<Vehiclelist, VehicleViewEntryDto>(vehicleList);
        }

        public override async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            var sichtungen = await context.SightingLists.ToListAsync();
            return [.. sichtungen.Select(ToDto)];
        }

        public override async Task<List<ContextDto>> GetKontextesAsync()
        {
            var contextList = await context.Contexts.ToListAsync();
            return mapper.MapList<Context, ContextDto>(contextList);
        }

        public override async Task AddSichtungAsync(SightingDto newSichtung)
        {
            await context.Sightings.AddAsync(mapper.MapSingle<SightingDto, Sighting>(newSichtung));
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

        public override async Task UpdateContext(ContextDto dto, UpdateMode updateMode)
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
