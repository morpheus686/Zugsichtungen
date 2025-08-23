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
        protected DataServiceBase(DbContext context)
        {
            this.context = context;
        }

        private readonly DbContext context;
        public abstract Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync();
        public abstract Task<List<ContextDto>> GetKontextesAsync();
        public abstract Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);
        public abstract Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId);

        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            Debug.WriteLine("Affected rows: " + affected);
        }

        public abstract Task<bool> DeleteSightingAsync(int sightingId);
        public abstract Task<List<SightingPictureDto>> GetAllSightingPicturesAsync();
        public abstract Task AddAsync(Sighting sighting);
        public abstract Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync();
        public abstract Task<List<Context>> GetContextesAsync();
        public abstract Task<List<VehicleViewEntry>> GetVehiclesAsync();
    }
}
