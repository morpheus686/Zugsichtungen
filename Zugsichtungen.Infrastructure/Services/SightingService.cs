using AutoMapper;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;

namespace Zugsichtungen.Infrastructure.Services
{
    public class SightingService(IDataService dataService, IMapper mapper) : ISightingService
    {
        public async Task AddSichtungAsync(DateOnly date, int vehicleId, int kontextId, string place, string? note, string? filePath)
        {


            var newSighting = new SightingDto
            {
                VehicleId = vehicleId,
                ContextId = kontextId,
                Location = place,
                Date = date,
                Note = note
            };

            SightingPictureDto? sightingPictureDto = null;

            if (filePath != null)
            {
                sightingPictureDto = new SightingPictureDto();
                sightingPictureDto.Filename = new FileInfo(filePath).Name;
                sightingPictureDto.Image = await File.ReadAllBytesAsync(filePath);
                               
            }
            
            await this.AddSichtungAsync(newSighting, sightingPictureDto); 
        }


        private async Task AddSichtungAsync(SightingDto sightingDto, SightingPictureDto? sightingPictureDto)
        {
            await dataService.AddSichtungAsync(sightingDto, sightingPictureDto);
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
    }
}
