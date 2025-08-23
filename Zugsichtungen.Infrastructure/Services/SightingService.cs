using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SightingService : ISightingService
    {
        public SightingService(IDataService dataService, IMapper mapper)
        {
            this.dataService = dataService;
            this.mapper = mapper;
        }


        private readonly IDataService dataService;
        private readonly IMapper mapper;

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

        public async Task<List<Context>> GetAllContextesAsync()
        {
            var contextList = await dataService.GetKontextesAsync();
            var pocoList = new List<Context>();

            foreach (var item in contextList)
            {
                pocoList.Add(mapper.Map<Context>(item));
            }

            return pocoList;
        }

        public async Task<List<VehicleViewEntry>> GetAllVehicleViewEntriesAsync()
        {
            var vehicleList = await dataService.GetAllFahrzeugeAsync();
            var pocoList = new List<VehicleViewEntry>();

            foreach (var item in vehicleList)
            {
                pocoList.Add(mapper.Map<VehicleViewEntry>(item));
            }

            return pocoList;
        }

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }

        public async Task<SightingPicture?> GetSightingPictureByIdAsync(int id)
        {
            var dto = await dataService.GetSightingPictureBySightingIdAsync(id);

            if (dto == null)
            {
                return null;
            }

            return mapper.Map<SightingPicture>(dto);
        }

        public Task<bool> CheckIfPictureExists(int sightingId)
        {
            return this.dataService.CheckIfSightingPictureExists(sightingId);
        }

        public async Task AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            var newSighting = Sighting.Create(-1, sighting.VehicleId, sighting.Date, sighting.Location, sighting.ContextId, sighting.Note);
            SightingPicture? newSightingPicture = null;

            if (sightingPicture != null)
            {
                newSightingPicture = SightingPicture.Create(-1, newSighting.Id, sightingPicture.Image, null, sightingPicture.Filename);
                newSighting.AddPicture(newSightingPicture);
            }

            await dataService.AddAsync(newSighting);
        }
    }
}
