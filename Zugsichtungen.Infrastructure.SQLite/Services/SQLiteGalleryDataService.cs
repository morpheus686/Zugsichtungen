using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Services
{
    public class SQLiteGalleryDataService : DataServiceBase, IGalleryDataService
    {
        private readonly ZugbeobachtungenContext context;

        public SQLiteGalleryDataService(ZugbeobachtungenContext context, ILogger<SQLiteGalleryDataService> logger)
            : base(context, logger)
        {
            this.context = context;
        }

        public async Task<List<Picture>> GetPicturesAsync()
        {
            var pictures = await GetAllWithLoggingAsync<Bild, List<Picture>>(async () =>
            {
                var fetchedPictures = new List<Picture>();

                foreach (var bild in await context.Bilds.ToListAsync())
                {
                    fetchedPictures.Add(MapFromEntity(bild));
                }

                return fetchedPictures;
            });

            return pictures;
        }

        private static Picture MapFromEntity(Bild entity)
        {
            return Picture.Create(
                entity.Id, entity.Datum, entity.Loknummer, entity.Ort, entity.Thema, entity.Bemerkung, entity.Bild1, entity.Thumbnail);
        }
    }
}
