using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Services
{
    public class SQLiteDataService : DataServiceBase
    {
        private readonly ZugbeobachtungenContext context;
        private readonly IImageRepository imageRepository;

        public SQLiteDataService(ZugbeobachtungenContext context,
            ILogger<SQLiteDataService> logger,
            IImageRepository imageRepository) : base(context, logger)
        {
            this.context = context;
            this.imageRepository = imageRepository;
        }

        public override Task UpdateContext(ContextDto updateContext, UpdateMode updateMode)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> DeleteSightingAsync(int sightingId)
        {
            var Sighting = await this.context.Sichtungens
                .Include(s => s.SichtungBilds)
                .FirstOrDefaultAsync(s => s.Id == sightingId);

            if (Sighting == null)
            {
                return false;
            }

            this.context.Remove(Sighting);
            return true;
        }

        // ab hier sind die Methoden, die nach dem DDD implementiert sind

        public override Task<int> AddAsync(Sighting sighting)
        {
            return AddWithLoggingAsync<Sichtungen>(
                async () =>
                {
                    var entity = MapToEntity(sighting);
                    await this.context.Sichtungens.AddAsync(entity);
                    await SaveChangesAsync();

                    return entity.Id;
                });
        }

        private static Sichtungen MapToEntity(Sighting sighting)
        {
            var entity = new Sichtungen
            {
                Datum = sighting.Date,
                Ort = sighting.Location,
                FahrzeugId = sighting.VehicleId,
                KontextId = sighting.ContextId,
                Bemerkung = sighting.Note
            };

            var sightingPicture = sighting.SightingPicture;

            if (sightingPicture != null)
            {
                entity.SichtungBilds.Add(new SichtungBild
                {
                    Bild = sightingPicture.Image,
                    Dateiname = sightingPicture.Filename,
                    Thumbnail = sightingPicture.Thumbnail,
                });
            }

            return entity;
        }

        public async override Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync()
        {
            var sightingViewEntryList = await GetAllWithLoggingAsync<Sichtungsview, List<SightingViewEntry>>(async () =>
            {
                var fetchedSightings = await context.Sichtungsviews.OrderBy(k => k.Loknummer).ThenBy(k => k.Datum).ToListAsync();
                var sightingViewEntries = new List<SightingViewEntry>();

                foreach (var item in fetchedSightings)
                {
                    sightingViewEntries.Add(MapFromEntity(item));
                }

                return sightingViewEntries;
            });

            return sightingViewEntryList;
        }


        private static SightingViewEntry MapFromEntity(Sichtungsview entity)
        {
            return SightingViewEntry.Create(entity.Id, entity.Datum, entity.Loknummer, entity.Ort, entity.Thema, entity.Bemerkung, null);
        }

        public async override Task<List<Context>> GetContextsAsync()
        {
            var contextList = await GetAllWithLoggingAsync<Kontexte, List<Context>>(async () =>
            {
                var contextEntities = await context.Kontextes.OrderBy(k => k.Name).ToListAsync();
                var contexts = new List<Context>();

                foreach (var entity in contextEntities)
                {
                    contexts.Add(MapFromEntity(entity));
                }

                return contexts;
            });

            return contextList;
        }

        private static Context MapFromEntity(Kontexte entity)
        {
            return Context.Create(entity.Id, entity.Name);
        }

        public async override Task<List<VehicleViewEntry>> GetVehicleViewEntriesAsync()
        {
            var vehicleViewEntryList = await GetAllWithLoggingAsync<Fahrzeugliste, List<VehicleViewEntry>>(async () =>
            {
                var vehicleEntities = await context.Fahrzeuglistes.OrderBy(k => k.Fahrzeug).ToListAsync();
                var vehicles = new List<VehicleViewEntry>();

                foreach (var entity in vehicleEntities)
                {
                    vehicles.Add(MapFromEntity(entity));
                }

                return vehicles;
            });

            return vehicleViewEntryList;
        }

        private static VehicleViewEntry MapFromEntity(Fahrzeugliste entity)
        {
            return VehicleViewEntry.Create(entity.Id, entity.Fahrzeug, entity.BaureiheId);
        }

        public override async Task<SightingPicture?> GetPictureBySightingIdAsync(int sightingId)
        {
            var domain = await GetWithLoggingAsync<SichtungBild, SightingPicture?>(
                sightingId,
                this.imageRepository.GetImageBySightingIdAsync
            );

            return domain;
        }

        public async override Task<SightingViewEntry?> GetSightingViewEntryAsync(int sightingId)
        {
            var domain = await GetWithLoggingAsync<Sichtungsview, SightingViewEntry?>(sightingId,
                async id =>
                {
                    var item = await context.Sichtungsviews.FirstOrDefaultAsync(entity => entity.Id == id);

                    if (item == null) return null;

                    return MapFromEntity(item);
                });

            return domain;
        }
    }
}
