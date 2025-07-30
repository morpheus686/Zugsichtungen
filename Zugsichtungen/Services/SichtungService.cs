using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Services
{
    public class SichtungService(IDataService dataService, IMapper mapper) : ISichtungService
    {
        public async Task AddSichtungAsync(DateOnly date, int? vehicleId, int? kontextId, string place, string? note)
        {
            var newSighting = new SightingDto
            {
                VehicleId = vehicleId,
                ContextId = kontextId,
                Location = place,
                Date = date,
                Note = note
            };

            await dataService.AddSichtungAsync(newSighting);
            await dataService.SaveChangesAsync();            
        }

        public async Task<List<SightingViewEntry>> GetAllSightingsAsync()
        {
            var sightingList = await dataService.GetSichtungenAsync();
            var pocoList = new List<SightingViewEntry>();

            foreach (var item in sightingList)
            {
                pocoList.Add(mapper.Map<SightingViewEntry>(item));
            }

            return pocoList;
        }
    }
}
