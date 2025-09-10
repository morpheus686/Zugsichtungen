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
        protected DataServiceBase(DbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        private readonly DbContext context;
        private readonly ILogger logger;

        public abstract Task UpdateContext(ContextDto updateContext, UpdateMode updateMode);

        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            string message = "Affected rows: " + affected;
            Debug.WriteLine(message);
            logger.LogDebug(message);
        }

        public abstract Task<bool> DeleteSightingAsync(int sightingId);
        public abstract Task<int> AddAsync(Sighting sighting);
        public abstract Task<List<SightingViewEntry>> GetAllSightingViewEntriesAsync();
        public abstract Task<List<Context>> GetContextsAsync();
        public abstract Task<List<VehicleViewEntry>> GetVehicleViewEntriesAsync();
        public abstract Task<SightingPicture?> GetPictureBySightingIdAsync(int sightingId);
        public abstract Task<SightingViewEntry?> GetSightingViewEntryAsync(int sightingId);

        protected async Task<int> AddWithLoggingAsync<TEntity>(Func<Task<int>> addFunc)
        {
            logger.LogDebug("adding {Entity} to database", nameof(TEntity));
            var id = await addFunc();
            logger.LogDebug("Added {Entity} to database with id {id}", nameof(TEntity), id);

            return id;
        }

        protected async Task<TDomain> GetWithLoggingAsync<TEntity, TDomain>(int id, Func<int, Task<TDomain>> fetchFunc)
        {
            logger.LogDebug("Fetching {Entity} with id {id} from database", nameof(TEntity), id);
            var domain = await fetchFunc(id);
            logger.LogDebug("Fetched {Entity} from database with id {id}", nameof(TEntity), id);
            return domain;
        }

        protected async Task<TListOfDomain> GetAllWithLoggingAsync<TEntity, TListOfDomain>(Func<Task<TListOfDomain>> fetchFunc)
        {
            logger.LogDebug("Fetching all {entity} from database", nameof(TEntity));
            var domain = await fetchFunc();
            logger.LogDebug("Fetched all {Entity} from database", nameof(TEntity));
            return domain;
        }
    }
}
