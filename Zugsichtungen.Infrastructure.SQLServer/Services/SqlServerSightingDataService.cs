using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models.Sighting;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Services
{
    public class SqlServerSightingDataService : DataServiceBase, ISightingDataService
    {
        private readonly TrainspottingContext context;
        private readonly ILogger<SqlServerSightingDataService> logger;
        private readonly IImageRepository imageRepository;

        public SqlServerSightingDataService(TrainspottingContext context,
            ILogger<SqlServerSightingDataService> logger,
            IImageRepository imageRepository) : base(context, logger)
        {
            this.context = context;
            this.logger = logger;
            this.imageRepository = imageRepository;
        }

        public async Task<int> AddAsync(Domain.Models.Sighting.Sighting sighting)
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

        private static Models.Sighting MapToEntity(Domain.Models.Sighting.Sighting sighting)
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

        public async Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync()
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
            return SightingViewEntry.Create(
                entity.Id, entity.SightingDate, entity.VehicleNumber, entity.Location, null, entity.Comment, null, null,
                entity.VehicleId, entity.SeriesId);
        }

        public async Task<List<Domain.Models.Sighting.Context>> GetContextsAsync()
        {
            var contextEntities = await context.Contexts.ToListAsync();
            var contextes = new List<Domain.Models.Sighting.Context>();

            foreach (var entity in contextEntities)
            {
                contextes.Add(MapFromEntity(entity));
            }

            return contextes;
        }

        private static Domain.Models.Sighting.Context MapFromEntity(Models.Context entity)
        {
            return Domain.Models.Sighting.Context.Create(entity.Id, entity.Description);
        }

        public async Task<List<VehicleViewEntry>> GetVehicleViewEntriesAsync()
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

        public async Task<Domain.Models.Sighting.SightingPicture?> GetPictureBySightingIdAsync(int sightingId)
        {
            var sightingPicture = await GetWithLoggingAsync<Models.SightingPicture, Domain.Models.Sighting.SightingPicture?>(
                sightingId,
                this.imageRepository.GetImageBySightingIdAsync);
            return sightingPicture;
        }

        public async Task<SightingViewEntry?> GetSightingViewEntryAsync(int sightingId)
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


        public async Task<List<Domain.Models.Sighting.Series>> GetAllSeriesAsync()
        {
            var seriesList = await GetAllWithLoggingAsync<Models.Series, List<Domain.Models.Sighting.Series>>(async () =>
            {
                var seriesEntities = await context.Series.OrderBy(e => e.Number).ToListAsync();
                var series = new List<Domain.Models.Sighting.Series>();

                foreach (var entity in seriesEntities)
                {
                    series.Add(MapFromEntity(entity));
                }

                return series;
            });

            return seriesList;
        }

        private static Domain.Models.Sighting.Series MapFromEntity(Models.Series entity)
        {
            return Domain.Models.Sighting.Series.Create(entity.Id, entity.Number, entity.Comment, entity.ModelId);
        }

        public async Task<List<Domain.Models.Sighting.Vehicle>> GetAllVehiclesAsync()
        {
            var vehicleList = await GetAllWithLoggingAsync<Models.Vehicle, List<Domain.Models.Sighting.Vehicle>>(async () =>
            {
                var vehicleEntities = await context.Vehicles.OrderBy(e => e.Number).ToListAsync();
                var vehicles = new List<Domain.Models.Sighting.Vehicle>();

                foreach (var entity in vehicleEntities)
                {
                    vehicles.Add(MapFromEntity(entity));
                }

                return vehicles;
            });

            return vehicleList;
        }

        private static Domain.Models.Sighting.Vehicle MapFromEntity(Models.Vehicle entity)
        {
            return Domain.Models.Sighting.Vehicle.Create(entity.Id, entity.Number, entity.SeriesId, entity.Comment);
        }
    }
}
