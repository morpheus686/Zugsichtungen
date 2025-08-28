using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Rest.Services
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

        public async Task AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            var sightingWithPictureDto = new SightingWithPictureDto()
            {
                Picture = sightingPicture,
                Sighting = sighting
            };

            await this.httpClient.PostAsJsonAsync("odata/SightingWithPicture", sightingWithPictureDto);
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
    }
}
