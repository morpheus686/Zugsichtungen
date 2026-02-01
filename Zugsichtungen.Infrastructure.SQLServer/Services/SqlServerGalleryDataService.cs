using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;

namespace Zugsichtungen.Infrastructure.SQLServer.Services
{
    public class SqlServerGalleryDataService : DataServiceBase, IGalleryDataService
    {
        private readonly TrainspottingContext context;

        public SqlServerGalleryDataService(TrainspottingContext context,
            ILogger<SqlServerGalleryDataService> logger,
            IImageRepository imageRepository) : base(context, logger)
        {
            this.context = context;
        }

        public async Task<List<Picture>> GetPicturesAsync()
        {
            var pictures = await GetAllWithLoggingAsync<Gallery, List<Picture>>(async () =>
            {
                return await context.Galleries
                    .AsNoTracking()
                    .Select(g => Picture.Create(
                        g.Id,
                        g.SightingDate,
                        g.VehicleNumber,
                        g.Location,
                        g.ContextDescription,
                        g.Comment,
                        g.Image,
                        g.Thumbnail))
                    .ToListAsync();
            });

            return pictures;
        }
    }
}
