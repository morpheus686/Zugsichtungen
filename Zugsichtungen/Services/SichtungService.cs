using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Services
{
    public class SichtungService : ISichtungService
    {
        private readonly IDataService dataService;

        public SichtungService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task AddSichtung(DateOnly date, int? vehicleId, int? kontextId, string place, string? note)
        {
            var newSighting = new Sighting
            {
                VehicleId = vehicleId,
                ContextId = kontextId,
                Location = place,
                Date = date,
                Note = note
            };

            await this.dataService.AddSichtungAsync(newSighting);
            await this.dataService.SaveChangesAsync();            
        }
    }
}
