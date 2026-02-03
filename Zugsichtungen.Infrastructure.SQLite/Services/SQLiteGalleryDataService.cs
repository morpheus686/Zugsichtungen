using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;

namespace Zugsichtungen.Infrastructure.SQLite.Services
{
    public class SQLiteGalleryDataService : DataServiceBase, IGalleryDataService
    {
        private readonly ZugbeobachtungenContext context;
        private readonly IImageRepository imageRepository;

        public SQLiteGalleryDataService(ZugbeobachtungenContext context,
            ILogger<SQLiteGalleryDataService> logger,
            IImageRepository imageRepository)
            : base(context, logger)
        {
            this.context = context;
            this.imageRepository = imageRepository;
        }

        public async Task<List<Picture>> GetPicturesAsync()
        {
            var pictures = await GetAllWithLoggingAsync<Bild, List<Picture>>(async () =>
            {
                return await context.Bildergaleries
                    .AsNoTracking()
                    .Select(b => Picture.Create(
                        b.SichtungId,
                        b.SichtungBildId,
                        b.Datum,
                        b.Loknummer,
                        b.Ort,
                        b.Thema,
                        b.Bemerkung))
                    .ToListAsync();
            });

            return pictures;
        }

        public Task<ThumbnailData?> GetThumbnailByIdAsync(int id)
        {
            return this.imageRepository.GetThumbnailByIdAsync(id);
        }
    }
}
