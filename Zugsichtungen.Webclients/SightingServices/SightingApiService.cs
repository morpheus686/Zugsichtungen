using System.Net.Http.Json;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Webclients.SightingService
{
    public class SightingApiService : ISightingService
    {
        private readonly HttpClient httpClient;

        public SightingApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            var response = await this.httpClient.PostAsJsonAsync("api/addsighting", new Tuple<SightingDto, SightingPictureDto?>(sighting, sightingPicture));
            int statusCode = Convert.ToInt32(response.StatusCode);
            return statusCode;
        }

        public async Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync()
        {
            var result = await this.httpClient.GetFromJsonAsync<List<SightingViewEntryDto>>("api/sightings");
            return result ?? new List<SightingViewEntryDto>();
        }

        public async Task<List<ContextDto>> GetContextesAsync()
        {
            var result = await this.httpClient.GetFromJsonAsync<List<ContextDto>>("api/contexts");
            return result ?? new List<ContextDto>();
        }

        public async Task<SightingPictureDto?> GetPictureBySightingIdAsync(int sightingId)
        {
            return await this.httpClient.GetFromJsonAsync<SightingPictureDto>($"api/sightingpicture?sightingId={sightingId}");
        }

        public Task<SightingViewEntryDto> GetSightingViewByIdAsync(int sightingId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync()
        {
            var result = await this.httpClient.GetFromJsonAsync<List<VehicleViewEntryDto>>("api/vehicleview");
            return result ?? new List<VehicleViewEntryDto>();
        }

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }
    }
}
