using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Infrastructure.Services
{
    public abstract class DataServiceBase : IDataService
    {
        protected DataServiceBase(DbContext context, ILogger<DataServiceBase> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        private readonly ILogger<DataServiceBase> logger;
        private readonly DbContext context;
        private readonly IMapper mapper;

        public abstract Task AddSightingAsync(SightingDto newSichtung, SightingPictureDto? sightingPictureDto);
        public abstract Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync();
        public abstract Task<List<ContextDto>> GetKontextesAsync();
        public abstract Task<List<SightingViewEntryDto>> GetSichtungenAsync();
        public abstract Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);
        public abstract Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId);
        public abstract Task<bool> CheckIfSightingPictureExists(int sightingId);

        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            Debug.WriteLine("Affected rows: " + affected);
        }

        public abstract Task<bool> DeleteSightingAsync(int sightingId);
        public abstract Task<List<SightingPictureDto>> GetAllSightingPicturesAsync();

        protected async Task AddSightingInternalAsync<TSighting, TSightingPicture>(
            SightingDto newSichtung,
            SightingPictureDto? sightingPictureDto,
            DbSet<TSighting> sightingSet,
            DbSet<TSightingPicture> sightingPicturesSet,
            string navigationPropertyName) 
            where TSighting : class
            where TSightingPicture : class
        {
            this.logger.LogInformation("Füge neue Sichtung zur Datenbank hinzu.");
            var newEntity = await sightingSet.AddAsync(mapper.Map<SightingDto, TSighting>(newSichtung));
            this.logger.LogInformation("Neue Sichtung angelegt.");

            if (sightingPictureDto != null)
            {
                var sightingPictureEntity = await sightingPicturesSet.AddAsync(mapper.Map<SightingPictureDto, TSightingPicture>(sightingPictureDto));
                typeof(TSightingPicture).GetProperty(navigationPropertyName).SetValue(sightingPictureEntity.Entity, newEntity.Entity);
            }

            await SaveChangesAsync();
        }
    }
}
