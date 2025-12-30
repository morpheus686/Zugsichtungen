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
            IImageRepository imageRepository) : base(context, logger)
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
            var id = await AddWithLoggingAsync<Models.Sighting?>(async () =>
            {
                var entity = MapToEntity(sighting);
                await this.context.Sightings.AddAsync(entity);
                await SaveChangesAsync();

                return entity.Id;
            });

            return id;
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
            var sightingViewEntryList = await GetAllWithLoggingAsync<SightingList, List<SightingViewEntry>>(async () =>
            {
                var sichtungen = await context.SightingLists.ToListAsync();
                var sightingList = new List<SightingViewEntry>();

                foreach (var item in sichtungen)
                {
                    sightingList.Add(MapFromEntity(item));
                }

                return sightingList;
            });

            return sightingViewEntryList;
        }

        private static SightingViewEntry MapFromEntity(SightingList entity)
        {
            return SightingViewEntry.Create(entity.Id, entity.SightingDate, entity.VehicleNumber, entity.Location, null, entity.Comment, null, null);
        }

        public async override Task<List<Domain.Models.Context>> GetContextsAsync()
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

        public async override Task<List<VehicleViewEntry>> GetVehicleViewEntriesAsync()
        {
            var vehicleViewEntryList = await GetAllWithLoggingAsync<Vehiclelist, List<VehicleViewEntry>>(async () =>
            {
                var vehicleEntities = await context.Vehiclelists.ToListAsync();
                var vehicles = new List<VehicleViewEntry>();

                foreach (var entity in vehicleEntities)
                {
                    vehicles.Add(MapFromEntity(entity));
                }

                return vehicles;
            });

            return vehicleViewEntryList;
        }

        private static VehicleViewEntry MapFromEntity(Vehiclelist entity)
        {
            return VehicleViewEntry.Create(entity.Id, entity.VehicleDesignation, entity.SeriesId);
        }

        public override async Task<Domain.Models.SightingPicture?> GetPictureBySightingIdAsync(int sightingId)
        {
            var sightingPicture = await GetWithLoggingAsync<Models.SightingPicture, Domain.Models.SightingPicture?>(
                sightingId,
                this.imageRepository.GetImageBySightingIdAsync);
            return sightingPicture;
        }

        public async override Task<SightingViewEntry?> GetSightingViewEntryAsync(int sightingId)
        {
            var sightingViewEntry = await GetWithLoggingAsync<SightingList, SightingViewEntry?>(
                sightingId,
                async id =>
                {
                    var item = await context.SightingLists.FirstOrDefaultAsync(entity => entity.Id == sightingId);

                    if (item == null) return null;

                    return MapFromEntity(item);
                });

            return sightingViewEntry;
        }

        public async override Task<List<Domain.Models.Series>> GetAllSeriesAsync()
        {
            var seriesList = await GetAllWithLoggingAsync<Zugsichtungen.Infrastructure.SQLServer.Models.Series, List<Domain.Models.Series>>(async () =>
            {
                var seriesEntities = await context.Series.OrderBy(e => e.Number).ToListAsync();
                var series = new List<Domain.Models.Series>();

                foreach (var entity in seriesEntities)
                {
                    series.Add(MapFromEntity(entity));
                }

                return series;
            });

            return seriesList;
        }

        private static Zugsichtungen.Domain.Models.Series MapFromEntity(Zugsichtungen.Infrastructure.SQLServer.Models.Series entity)
        {
            return Zugsichtungen.Domain.Models.Series.Create(entity.Id, entity.Number, entity.Comment, entity.ModelId);
        }

        public async override Task<List<Domain.Models.Vehicle>> GetAllVehiclesAsync()
        {
            var vehicleList = await GetAllWithLoggingAsync<Zugsichtungen.Infrastructure.SQLServer.Models.Vehicle, List<Domain.Models.Vehicle>>(async () =>
            {
                var vehicleEntities = await context.Vehicles.OrderBy(e => e.Number).ToListAsync();
                var vehicles = new List<Domain.Models.Vehicle>();

                foreach (var entity in vehicleEntities)
                {
                    vehicles.Add(MapFromEntity(entity));
                }

                return vehicles;
            });

            return vehicleList;
        }

        private static Zugsichtungen.Domain.Models.Vehicle MapFromEntity(Zugsichtungen.Infrastructure.SQLServer.Models.Vehicle entity)
        {
            return Zugsichtungen.Domain.Models.Vehicle.Create(entity.Id, entity.Number, entity.SeriesId, entity.Comment);
        }
    }
}
