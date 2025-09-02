using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Services
{
    public class SqlServerDataService : DataServiceBase
    {
        private readonly TrainspottingContext context;
        private readonly ILogger<SqlServerDataService> logger;
        private readonly IImageRepository imageRepository;

        public SqlServerDataService(TrainspottingContext context,
            ILogger<SqlServerDataService> logger,
            IImageRepository imageRepository) : base(context)
        {
            this.context = context;
            this.logger = logger;
            this.imageRepository = imageRepository;
        }

        public override Task UpdateContext(ContextDto updateContext, UpdateMode updateMode)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteSightingAsync(int sightingId)
        {
            throw new NotImplementedException();
        }

        public async override Task<int> AddAsync(Domain.Models.Sighting sighting)
        {
            var entity = MapToEntity(sighting);
            await this.context.Sightings.AddAsync(entity);
            await SaveChangesAsync();

            return entity.Id;
        }

        private static Models.Sighting MapToEntity(Domain.Models.Sighting sighting)
        {
            var entity = new Models.Sighting
            {
                Date = sighting.Date,
                Location = sighting.Location,
                VehicleId = sighting.VehicleId,
                ContextId = sighting.ContextId,
                Comment = sighting.Note
            };

            var sightingPicture = sighting.SightingPicture;

            if (sightingPicture != null)
            {
                entity.SightingPictures.Add(new Models.SightingPicture
                {
                    Image = sightingPicture.Image,
                    Filename = sightingPicture.Filename
                });
            }

            return entity;
        }

        public async override Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync()
        {
            var sichtungen = await context.SightingLists.ToListAsync();
            var sightingList = new List<SightingViewEntry>();

            foreach (var item in sichtungen)
            {
                sightingList.Add(MapFromEntity(item));
            }

            return sightingList;
        }

        private static SightingViewEntry MapFromEntity(SightingList entity)
        {
            return SightingViewEntry.Create(entity.Id, entity.SightingDate, entity.VehicleNumber, entity.Location, null, entity.Comment, null);
        }

        public async override Task<List<Domain.Models.Context>> GetContextesAsync()
        {
            var contextEntities = await context.Contexts.ToListAsync();
            var contextes = new List<Domain.Models.Context>();

            foreach (var entity in contextEntities)
            {
                contextes.Add(MapFromEntity(entity));
            }

            return contextes;
        }

        private static Domain.Models.Context MapFromEntity(Models.Context entity)
        {
            return Domain.Models.Context.Create(entity.Id, entity.Description);
        }

        public async override Task<List<VehicleViewEntry>> GetVehiclesAsync()
        {
            var vehicleEntities = await context.Vehiclelists.ToListAsync();
            var vehicles = new List<VehicleViewEntry>();

            foreach (var entity in vehicleEntities)
            {
                vehicles.Add(MapFromEntity(entity));
            }

            return vehicles;
        }

        private static VehicleViewEntry MapFromEntity(Vehiclelist entity)
        {
            return VehicleViewEntry.Create(entity.Id, entity.VehicleDesignation, entity.SeriesId);
        }

        public override Task<Domain.Models.SightingPicture?> GetPictureBySightingIdAsync(int sightingId)
        {
            return this.imageRepository.GetImageBySightingIdAsync(sightingId);
        }

        public async override Task<SightingViewEntry?> GetSightingViewAsync(int sightingId)
        {
            var item = await context.SightingLists.FirstOrDefaultAsync(entity =>  entity.Id == sightingId);

            if (item == null) return null;

            return MapFromEntity(item);
        }
    }
}
