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
                var fetchedPictures = new List<Picture>();

                foreach (var galleryItem in await context.Galleries.AsNoTracking().ToListAsync())
                {
                    fetchedPictures.Add(MapFromEntity(galleryItem));
                }

                return fetchedPictures;
            });

            return pictures;
        }

        private static Picture MapFromEntity(Gallery entity)
        {
            return Picture.Create(
                entity.Id, entity.SightingDate, entity.VehicleNumber, entity.Location, entity.ContextDescription, entity.Comment, entity.Image, entity.Thumbnail);
        }
    }
}
