using System.Net.Http.Json;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Webclients.GalleryServices
{
    public class GalleryApiService : IGalleryService
    {
        private readonly HttpClient httpClient;

        public GalleryApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<PictureDto>> GetGalleryPicturesAsync()
        {
            var result = await this.httpClient.GetFromJsonAsync<List<PictureDto>>("api/pictures");
            return result ?? new List<PictureDto>();
        }

        public async Task<ThumbnailDataDto?> GetThumbnailDataAsync(int pictureId)
        {
            return await this.httpClient.GetFromJsonAsync<ThumbnailDataDto>($"api/thumbnail?pictureId={pictureId}");
        }
    }
}
