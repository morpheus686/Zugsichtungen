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
            var sightingWithPictureDto = new SightingWithPictureDto()
            {
                Picture = sightingPicture,
                Sighting = sighting
            };

            var response = await this.httpClient.PostAsJsonAsync("odata/SightingWithPicture", sightingWithPictureDto);
            int statusCode = Convert.ToInt32(response.StatusCode);
            return statusCode;
        }

        public async Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<SightingViewEntryDto>>("odata/Sighting");
            return response?.Value ?? new List<SightingViewEntryDto>();
        }

        public async Task<List<ContextDto>> GetContextesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<ContextDto>>("odata/Context");
            return response?.Value ?? new List<ContextDto>();
        }

        public async Task<SightingPictureDto?> GetPictureBySightingIdAsync(int sightingId)
        {
            return await httpClient.GetFromJsonAsync<SightingPictureDto>($"odata/SightingPicture({sightingId})");
        }

        public async Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ODataResponse<VehicleViewEntryDto>>("odata/Vehicle");
            return response?.Value ?? new List<VehicleViewEntryDto>();
        }

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }

        public Task<SightingViewEntryDto> GetSightingViewByIdAsync(int sightingId)
        {
            throw new NotImplementedException();
        }
    }
}
