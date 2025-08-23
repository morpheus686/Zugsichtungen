using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.Mapping;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Services
{
    public class SQLiteDataService : DataServiceBase
    {
        private readonly ZugbeobachtungenContext context;
        private readonly IMapper mapper;
        private readonly ILogger<SQLiteDataService> logger;
        private readonly IImageRepository imageRepository;

        public SQLiteDataService(ZugbeobachtungenContext context,
            IMapper mapper,
            ILogger<SQLiteDataService> logger,
            IImageRepository imageRepository) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.imageRepository = imageRepository;
        }

        public override async Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync()
        {
            var fahrzeuge = await context.Fahrzeuglistes.ToListAsync();
            return mapper.MapList<Fahrzeugliste, VehicleViewEntryDto>(fahrzeuge);
        }

        public override async Task<List<SightingViewEntryDto>> GetSichtungenAsync()
        {
            var sichtungen = await context.Sichtungsviews.ToListAsync();
            return mapper.MapList<Sichtungsview, SightingViewEntryDto>(sichtungen);
        }

        public override async Task<List<ContextDto>> GetKontextesAsync()
        {
            var kontexte = await context.Kontextes.ToListAsync();
            return mapper.MapList<Kontexte, ContextDto>(kontexte);
        }

        public override Task UpdateContext(ContextDto updateContext, UpdateMode updateMode)
        {
            throw new NotImplementedException();
        }

        public override Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId)
        {
            return this.imageRepository.GetImageBySightingIdAsync(sightingId);
        }

        public override Task<bool> CheckIfSightingPictureExists(int sightingId)
        {
            return this.imageRepository.CheckIfImageExistsAsync(sightingId);
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

        public override Task<List<SightingPictureDto>> GetAllSightingPicturesAsync()
        {
            return this.context.SichtungBilds
              .AsNoTracking() // keine Change-Tracking-Overhead
              .Select(b => new SightingPictureDto
              {
                  Id = b.Id,
                  SightingId = b.SichtungId,
                  Filename = b.Dateiname,
                  Thumbnail = b.Thumbnail
              })
              .ToListAsync();
        }

        public override async Task AddAsync(Sighting sighting)
        {
            var entity = MapToEntity(sighting);
            await this.context.Sichtungens.AddAsync(entity);
            await SaveChangesAsync();
        }

        private Sichtungen MapToEntity(Sighting sighting)
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

        private SightingViewEntry MapFromEntity(Sichtungsview entity)
        {
            return SightingViewEntry.Create(entity.Id, entity.Datum, entity.Loknummer, entity.Ort, entity.Thema, entity.Bemerkung, null);
        }
    }
}
