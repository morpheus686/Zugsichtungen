using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Zugsichtungen.Infrastructure.Services
{
    public abstract class DataServiceBase
    {
        protected DataServiceBase(DbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        private readonly DbContext context;
        private readonly ILogger logger;

        public async Task SaveChangesAsync()
        {
            var affected = await context.SaveChangesAsync();
            string message = "Affected rows: " + affected;
            Debug.WriteLine(message);
            logger.LogDebug(message);
        }

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
