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
                return await context.Bilds
                    .AsNoTracking()
                    .Select(b => Picture.Create(
                        b.Id,
                        b.Datum,
                        b.Loknummer,
                        b.Ort,
                        b.Thema,
                        b.Bemerkung,
                        b.Bild1,
                        b.Thumbnail))
                    .ToListAsync();
            });

            return pictures;
        }
    }
}
