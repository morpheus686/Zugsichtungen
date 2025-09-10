using AutoMapper;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.Mapping;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SightingService : ISightingService
    {
        public SightingService(IDataService dataService, IMapper mapper, ILogger<SightingService> logger)
        {
            this.dataService = dataService;
            this.mapper = mapper;
            this.logger = logger;
        }

        private readonly IDataService dataService;
        private readonly IMapper mapper;
        private readonly ILogger<SightingService> logger;

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }

        // ab hier sind die Methoden, die nach dem DDD implementiert sind

        public async Task<int> AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            try
            {
                logger.LogInformation("Adding new sighting in {Location} for {VehicleId} at {Date}.", sighting.Location, sighting.VehicleId, sighting.Date);

                var newSighting = Sighting.Create(-1, sighting.VehicleId, sighting.Date, sighting.Location, sighting.ContextId, sighting.Note);
                SightingPicture? newSightingPicture = null;

                if (sightingPicture != null)
                {
                    newSightingPicture = SightingPicture.Create(-1, newSighting.Id, sightingPicture.Image, null, sightingPicture.Filename);
                    newSighting.AddPicture(newSightingPicture);
                }

                var id = await dataService.AddAsync(newSighting);
                logger.LogInformation("SightingAdded with Id {SightingId}.", id);

                return id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add sighting for VehicleId {VehicleId}", sighting.VehicleId);
                throw;
            }
        }

        public async Task<List<SightingViewEntryDto>> GetAllSightingViewEntriesAsync()
        {
            try
            {
                logger.LogInformation("Fetching all sighting view entries.");
                var sightingList = await this.dataService.GetAllSightingViewEntriesAsync();
                logger.LogInformation("Fetched {Count} sighting view entries.", sightingList.Count);
                return mapper.MapList<SightingViewEntry, SightingViewEntryDto>(sightingList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch all sighting view entries");
                throw;
            }
        }

        public async Task<List<ContextDto>> GetContextsAsync()
        {
            try
            {
                logger.LogInformation("Fetching all contexts.");
                var contexts = await this.dataService.GetContextsAsync();
                logger.LogInformation("Fetched {Count} sighting view entries.", contexts.Count);
                return mapper.MapList<Context, ContextDto>(contexts);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch all contexts.");
                throw;
            }
        }

        public async Task<List<VehicleViewEntryDto>> GetVehicleViewEntriesAsync()
        {
            try
            {
                logger.LogInformation("Fetching all vehicle view entries.");
                var vehicles = await this.dataService.GetVehicleViewEntriesAsync();
                logger.LogInformation("Fetched {Count} vehicle view entries.", vehicles.Count);
                return mapper.MapList<VehicleViewEntry, VehicleViewEntryDto>(vehicles);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch all vehicle view entries.");
                throw;
            }
        }

        public async Task<SightingPictureDto?> GetSightingPictureBySightingIdAsync(int sightingId)
        {
            try
            {
                logger.LogInformation("Fetching sighting picture by sighting id {Id}.", sightingId);
                var sightingPicture = await dataService.GetPictureBySightingIdAsync(sightingId);
                logger.LogInformation("Fetched sighting picture by sighting id {Id}.", sightingId);
                return mapper.Map<SightingPictureDto>(sightingPicture);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to sighting picture with id {Id}.", sightingId);
                throw;
            }
        }

        public async Task<SightingViewEntryDto> GetSightingViewEntryBySightingIdAsync(int sightingId)
        {
            try
            {
                logger.LogInformation("Fetching sighting view entry with id {Id}", sightingId);
                var sightingViewEntry = await this.dataService.GetSightingViewEntryAsync(sightingId);
                logger.LogInformation("Fetched sighting view entry with id {Id}", sightingId);
                return mapper.Map<SightingViewEntryDto>(sightingViewEntry);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to sighting view entry with id {Id}.", sightingId);
                throw;
            }
        }
    }
}
