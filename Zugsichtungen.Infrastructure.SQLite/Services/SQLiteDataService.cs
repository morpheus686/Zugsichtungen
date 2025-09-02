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
        private readonly ILogger<SQLiteDataService> logger;
        private readonly IImageRepository imageRepository;

        public SQLiteDataService(ZugbeobachtungenContext context,
            ILogger<SQLiteDataService> logger,
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

        public override async Task<bool> DeleteSightingAsync(int sightingId)
        {
            var sichtung = await this.context.Sichtungens
                .Include(s => s.SichtungBilds)
                .FirstOrDefaultAsync(s => s.Id == sightingId);

            if (sichtung == null)
            {
                return false;
            }

            this.context.Remove(sichtung);
            return true;
        }

        // ab hier sind die Methoden, die nach dem DDD implementiert sind

        public override async Task<int> AddAsync(Sighting sighting)
        {
            var entity = MapToEntity(sighting);
            await this.context.Sichtungens.AddAsync(entity);
            await SaveChangesAsync();

            return entity.Id;
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
            var sichtungen = await context.Sichtungsviews.ToListAsync();
            var sightingList = new List<SightingViewEntry>();

            foreach (var item in sichtungen)
            {
                sightingList.Add(MapFromEntity(item));
            }

            return sightingList;
        }

        private static SightingViewEntry MapFromEntity(Sichtungsview entity)
        {
            return SightingViewEntry.Create(entity.Id, entity.Datum, entity.Loknummer, entity.Ort, entity.Thema, entity.Bemerkung, null);
        }

        public async override Task<List<Context>> GetContextesAsync()
        {
            var contextEntities = await context.Kontextes.ToListAsync();
            var contextes = new List<Context>();

            foreach (var entity in contextEntities)
            {
                contextes.Add(MapFromEntity(entity));
            }

            return contextes;
        }

        private static Context MapFromEntity(Kontexte entity)
        {
            return Context.Create(entity.Id, entity.Name);
        }

        public async override Task<List<VehicleViewEntry>> GetVehiclesAsync()
        {
            var vehicleEntities = await context.Fahrzeuglistes.ToListAsync();
            var vehicles = new List<VehicleViewEntry>();

            foreach (var entity in vehicleEntities)
            {
                vehicles.Add(MapFromEntity(entity));
            }

            return vehicles;
        }

        private static VehicleViewEntry MapFromEntity(Fahrzeugliste entity)
        {
            return VehicleViewEntry.Create(entity.Id, entity.Fahrzeug, entity.BaureiheId);
        }

        public override Task<SightingPicture?> GetPictureBySightingIdAsync(int sightingId)
        {
            return this.imageRepository.GetImageBySightingIdAsync(sightingId);
        }

        public async override Task<SightingViewEntry?> GetSightingViewAsync(int sightingId)
        {
            var item = await context.Sichtungsviews.FirstOrDefaultAsync(entity =>  entity.Id == sightingId);

            if (item == null) return null;

            return MapFromEntity(item);
        }
    }
}
