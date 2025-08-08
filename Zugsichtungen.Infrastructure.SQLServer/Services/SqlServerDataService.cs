using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Foundation.Mapping;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Services
{
    public class SqlServerDataService : DataServiceBase
    {
        private readonly TrainspottingContext context;
        private readonly IMapper mapper;
        private readonly ILogger<SqlServerDataService> logger;
        private readonly IImageRepository imageRepository;

        public SqlServerDataService(TrainspottingContext context,
            IMapper mapper, 
            ILogger<SqlServerDataService> logger,
            IImageRepository imageRepository) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.imageRepository = imageRepository;
        }

        public override async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            this.logger.LogInformation("Rufe alle Triebfahrzeuge aus der Datenbank ab.");
            var vehicleList = await context.Vehiclelists.ToListAsync();
            return mapper.MapList<Vehiclelist, VehicleViewEntryDto>(vehicleList);
        }

        public override async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            this.logger.LogInformation("Rufe alle Sichtungen aus der Datenbank ab.");
            var sichtungen = await context.SightingLists.ToListAsync();
            return mapper.MapList<SightingList, SightingViewEntryDto>(sichtungen);
        }

        public override async Task<List<ContextDto>> GetKontextesAsync()
        {
            this.logger.LogInformation("Rufe alle Themen aus der Datenbank ab.");
            var contextList = await context.Contexts.ToListAsync();
            return mapper.MapList<Context, ContextDto>(contextList);
        }

        public override async Task AddSichtungAsync(SightingDto newSichtung, SightingPictureDto? sightingPictureDto)
        {
            this.logger.LogInformation("Füge neue Sichtung zur Datenbank hinzu.");
            var newEntity = await context.Sightings.AddAsync(mapper.MapSingle<SightingDto, Sighting>(newSichtung));
            this.logger.LogInformation("Neue Sichtung angelegt.");

            if (sightingPictureDto != null)
            {
                var sightingPictureEntity = await context.SightingPictures.AddAsync(mapper.Map<SightingPictureDto, SightingPicture>(sightingPictureDto));
                sightingPictureEntity.Entity.Sighting = newEntity.Entity;
            }
        }

        public override Task UpdateContext(ContextDto updateContext, UpdateMode updateMode)
        {
            throw new NotImplementedException();
        }

        //public override async Task UpdateContext(ContextDto dto, UpdateMode updateMode)
        //{
        //    switch (updateMode)
        //    {
        //        case UpdateMode.Full:
        //            await UpdateContextFullAsync(dto);
        //            break;

        //        case UpdateMode.Partial:
        //            await UpdateContextPartialAsync(dto);
        //            break;

        //        case UpdateMode.Tracked:
        //            await UpdateContextTrackedAsync(dto);
        //            break;
        //    }
        //}
        //private async Task UpdateContextFullAsync(ContextDto dto)
        //{
        //    var entity = new Context
        //    {
        //        Id = dto.Id,
        //        Description = dto.Name
        //    };

        //    context.Attach(entity);
        //    context.Entry(entity).State = EntityState.Modified;

        //    await context.SaveChangesAsync();
        //}

        //private async Task UpdateContextTrackedAsync(ContextDto dto)
        //{
        //    var entity = new Context { Id = dto.Id };

        //    context.Attach(entity);

        //    context.Entry(entity).Property(x => x.Description).CurrentValue = dto.Name;
        //    context.Entry(entity).Property(x => x.Description).IsModified = true;

        //    await context.SaveChangesAsync();
        //}

        //private async Task UpdateContextPartialAsync(ContextDto dto)
        //{
        //    var entity = await context.Contexts.FirstOrDefaultAsync(x => x.Id == dto.Id);
        //    if (entity is null) return;

        //    // Mappen nur der Felder, die sich ändern dürfen
        //    entity.Description = dto.Name;

        //    await context.SaveChangesAsync();
        //}
    }
}
