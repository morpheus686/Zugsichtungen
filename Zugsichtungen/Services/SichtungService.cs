using Zugsichtungen.Models;

namespace Zugsichtungen.Services
{
    public class SichtungService : ISichtungService
    {
        private readonly IDataService dataService;

        public SichtungService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task AddSichtung(DateOnly date, int? vehicleId, int? kontextId, string place, string note)
        {
            var newSichtung = new Sichtungen
            {
                FahrzeugId = vehicleId,
                KontextId = kontextId,
                Ort = place,
                Datum = date,
                Bemerkung = note
            };

            await this.dataService.AddSichtungAsync(newSichtung);
            await this.dataService.SaveChangesAsync();            
        }
    }
}
