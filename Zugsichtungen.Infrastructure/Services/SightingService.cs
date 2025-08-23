using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.Mapping;

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

        // ab hier sind die Methoden, die nach dem DDD implementiert sind

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

        public async Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync()
        {
            var sightingList = await this.dataService.GetAllSightingViewEntriesAsync();
            return mapper.MapList<SightingViewEntry, SightingViewEntryDto>(sightingList);
        }

        public async Task<List<ContextDto>> GetContextesAsync()
        {
            var contextes = await this.dataService.GetContextesAsync();
            return mapper.MapList<Context, ContextDto>(contextes);
        }

        public async Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync()
        {
            var vehicles = await this.dataService.GetVehiclesAsync();
            return mapper.MapList<VehicleViewEntry, VehicleViewEntryDto>(vehicles);
        }
    }
}
