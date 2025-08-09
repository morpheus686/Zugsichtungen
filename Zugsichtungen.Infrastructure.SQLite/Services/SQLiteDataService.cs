using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Interfaces;
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

        public override async Task AddSichtungAsync(SightingDto newSichtung, SightingPictureDto? sightingPictureDto)
        {
            this.logger.LogInformation("Füge neue Sichtung zur Datenbank hinzu.");
            var newEntity = await context.Sichtungens.AddAsync(mapper.MapSingle<SightingDto, Sichtungen>(newSichtung));
            this.logger.LogInformation("Neue Sichtung angelegt.");

            if (sightingPictureDto != null)
            {
                var sightingPictureEntity = await context.SichtungBilds.AddAsync(mapper.Map<SightingPictureDto, SichtungBild>(sightingPictureDto));
                sightingPictureEntity.Entity.Sichtung = newEntity.Entity;
            }
        }

        public override Task UpdateContext(ContextDto updateContext, UpdateMode updateMode)
        {
            throw new NotImplementedException();
        }

        public override Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId)
        {
            return this.imageRepository.GetImageBySightingIdAsync(sightingId);
        }
    }
}
