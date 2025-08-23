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

        public async Task AddSichtungAsync(DateOnly date, int vehicleId, int kontextId, string place, string? note, string? filePath)
        {
            var newSighting = new Sighting
            {
                VehicleId = vehicleId,
                ContextId = kontextId,
                Location = place,
                Date = date,
                Note = note
            };

            SightingPicture? sightingPictureDto = null;

            if (filePath != null)
            {
                var picture = await File.ReadAllBytesAsync(filePath);

                sightingPictureDto = new SightingPicture
                {
                    Filename = new FileInfo(filePath).Name,
                    Image = picture,
                    Thumbnail = ImageHelper.CreateThumbnail(picture)
                };
            }

            await this.AddSichtungAsync(newSighting, sightingPictureDto);
        }

        public async Task AddSichtungAsync(Sighting sighting, SightingPicture? sightingPicture)
        {
            await dataService.AddSightingAsync(mapper.Map<Sighting, SightingDto>(sighting),
                mapper.Map<SightingPicture?, SightingPictureDto?>(sightingPicture));
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
    }
}
