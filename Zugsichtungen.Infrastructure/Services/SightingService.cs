using AutoMapper;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models.Sighting;
using Zugsichtungen.Foundation.Mapping;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SightingService : ISightingService
    {
        public SightingService(ISightingDataService dataService, IMapper mapper, ILogger<SightingService> logger)
        {
            this.dataService = dataService;
            this.mapper = mapper;
            this.logger = logger;
        }

        private readonly ISightingDataService dataService;
        private readonly IMapper mapper;
        private readonly ILogger<SightingService> logger;

        public Task UpdateContextes(List<Context> contextes)
        {
            throw new NotImplementedException();
        }

        // ab hier sind die Methoden, die nach dem DDD implementiert sind

        public async Task<int> AddSightingAsync(SightingDto sighting, SightingPictureDto? sightingPicture)
        {
            var sightingWithPicture = new SightingWithPictureDto
            (
                sighting,
                sightingPicture
            );

            return await AddSightingAsync(sightingWithPicture);
        }

        public async Task<int> AddSightingAsync(SightingWithPictureDto sightingWithPicture)
        {
            try
            {
                logger.LogInformation("Adding new sighting in {Location} for {VehicleId} at {Date}.",
                               sightingWithPicture.Sighting.Location,
                               sightingWithPicture.Sighting.VehicleId,
                               sightingWithPicture.Sighting.Date);

                var newSighting = Sighting.Create(
                    sightingWithPicture.Sighting.Id, 
                    sightingWithPicture.Sighting.VehicleId,
                    sightingWithPicture.Sighting.Date,
                    sightingWithPicture.Sighting.Location,
                    sightingWithPicture.Sighting.ContextId,
                    sightingWithPicture.Sighting.Note);
                SightingPicture? newSightingPicture = null;

                if (sightingWithPicture.Picture != null)
                {
                    newSightingPicture = SightingPicture.Create(
                        sightingWithPicture.Picture.Id, 
                        newSighting.Id, 
                        sightingWithPicture.Picture.Image,
                        null,
                        sightingWithPicture.Picture.Filename);
                    newSighting.AddPicture(newSightingPicture);
                }

                var id = await dataService.AddAsync(newSighting);
                logger.LogInformation("SightingAdded with Id {SightingId}.", id);

                return id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, 
                    "Failed to add sighting for VehicleId {VehicleId}",
                    sightingWithPicture.Sighting.VehicleId);
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

        public async Task<List<SeriesDto>> GetAllSeriesAsync()
        {
            try
            {
                logger.LogInformation("Fetching all series entities.");
                var sightingViewEntry = await this.dataService.GetAllSeriesAsync();
                logger.LogInformation($"Fetched {sightingViewEntry.Count} series entities.");
                return mapper.MapList<Series, SeriesDto>(sightingViewEntry);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch all series entities");
                throw;
            }
        }

        public async Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            try
            {
                logger.LogInformation("Fetching all vehicle entities.");
                var vehicles = await this.dataService.GetAllVehiclesAsync();
                logger.LogInformation($"Fetched {vehicles.Count} vehicle entities.");
                return mapper.MapList<Vehicle, VehicleDto>(vehicles);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch all vehicle entities");
                throw;
            }
        }
    }
}
