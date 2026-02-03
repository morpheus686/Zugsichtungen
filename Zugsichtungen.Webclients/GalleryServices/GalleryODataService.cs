using System.Text.Json.Serialization;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Webclients.GalleryServices
{
    public class GalleryODataService : IGalleryService
    {
        private class ODataResponse<T>
        {
            [JsonPropertyName("value")]
            public List<T> Value { get; set; } = new();
        }

        private readonly HttpClient httpClient;

        public GalleryODataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<List<PictureDto>> GetGalleryPicturesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ThumbnailDataDto?> GetThumbnailDataAsync(int pictureId)
        {
            throw new NotImplementedException();
        }
    }
}
