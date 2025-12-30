using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Webclients.SightingService
{
    public class SightingODataService : ISightingService
    {
        private class ODataResponse<T>
        {
            [JsonPropertyName("value")]
            public List<T> Value { get; set; } = new();
        }

        private readonly HttpClient httpClient;

        public SightingODataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            var sightingWithPictureDto = new SightingWithPictureDto
            (
                sighting,
                sightingPicture

            );

            return await AddSightingAsync(sightingWithPictureDto);
        }

        public async Task<int> AddSightingAsync(SightingWithPictureDto sightingWithPicture)
        {
            var response = await this.httpClient.PostAsJsonAsync("odata/Sighting", sightingWithPicture);
            int statusCode = Convert.ToInt32(response.StatusCode);
            return statusCode;
        }

        public async Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<SightingViewEntryDto>>("odata/Sighting");
            return response?.Value ?? [];
        }

        public async Task<List<ContextDto>> GetContextsAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<ContextDto>>("odata/Context");
            return response?.Value ?? [];
        }

        public async Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId)
        {
            return await httpClient.GetFromJsonAsync<SightingPictureDto>($"odata/SightingPicture({sightingId})");
        }

        public async Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<VehicleViewEntryDto>>("odata/VehicleView");
            return response?.Value ?? [];
        }

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }

        public Task<SightingViewEntryDto> GetSightingViewEntryBySightingIdAsync(int sightingId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SeriesDto>> GetAllSeriesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<SeriesDto>>("odata/Series");
            return response?.Value ?? [];
        }

        public async Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<VehicleDto>>("odata/Vehicle");
            return response?.Value ?? [];
        }
    }
}
