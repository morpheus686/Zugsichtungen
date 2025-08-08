using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Enumerations.Database;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Infrastructure.Services
{
    public abstract class DataServiceBase(DbContext context) : IDataService
    {
        public abstract Task AddSichtungAsync(SightingDto newSichtung, SightingPictureDto? sightingPictureDto);
        public abstract Task<List<VehicleViewEntryDto>> GetAllFahrzeugeAsync();
        public abstract Task<List<ContextDto>> GetKontextesAsync();
        public abstract Task<List<SightingViewEntryDto>> GetSichtungenAsync();
        public abstract Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);

        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            Debug.WriteLine("Affected rows: " + affected);
        }
    }
}
