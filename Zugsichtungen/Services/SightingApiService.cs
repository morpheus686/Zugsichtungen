using System.Net.Http;
using System.Net.Http.Json;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Services
{
    public class SightingApiService : ISightingService
    {
        private readonly HttpClient httpClient;

        public SightingApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            await httpClient.PostAsJsonAsync("api/addsighting", new Tuple<SightingDto, SightingPictureDto?>(sighting, sightingPicture));
        }

        public async Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<SightingViewEntryDto>>("api/sightings");
            return result ?? new List<SightingViewEntryDto>();
        }

        public async Task<List<ContextDto>> GetContextesAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<ContextDto>>("api/contexts");
            return result ?? new List<ContextDto>();
        }

        public async Task<SightingPictureDto?> GetPictureBySightingIdAsync(int sightingId)
        {
            return await httpClient.GetFromJsonAsync<SightingPictureDto>($"api/sightingpicture?sightingId={sightingId}");
        }

        public async Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<VehicleViewEntryDto>>("api/vehicleview");
            return result ?? new List<VehicleViewEntryDto>();
        }

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }
    }
}
