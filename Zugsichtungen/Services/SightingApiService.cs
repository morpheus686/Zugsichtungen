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

        public Task AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<SightingViewEntryDto>>("api/sightings");
            return result ?? new List<SightingViewEntryDto>();
        }

        public Task<List<ContextDto>> GetContextesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SightingPictureDto?> GetPictureBySightingIdAsync(int sightingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }
    }
}
