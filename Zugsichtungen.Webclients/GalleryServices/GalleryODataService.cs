using System.Net.Http.Json;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Webclients.GalleryServices
{
    public class GalleryODataService : ODataServiceBase, IGalleryService
    {
        private readonly HttpClient httpClient;

        public GalleryODataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<PictureDto>> GetGalleryPicturesAsync()
        {
            var response = await this.httpClient.GetFromJsonAsync<ODataResponse<PictureDto>>("odata/Picture");
            return response?.Value ?? [];
        }

        public async Task<ThumbnailDataDto?> GetThumbnailDataAsync(int pictureId)
        {
            return await this.httpClient.GetFromJsonAsync<ThumbnailDataDto>($"odata/ThumbnailData({pictureId})");
        }
    }
}
