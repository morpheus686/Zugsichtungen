using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

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
        public abstract Task AddAsync(Sighting sighting);
    }
}
