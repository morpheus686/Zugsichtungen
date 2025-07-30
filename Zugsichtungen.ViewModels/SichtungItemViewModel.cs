using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels
{
    public class SichtungItemViewModel : ViewModelBase
    {
        private readonly SightingViewEntry sichtung;

        public int? Id => sichtung.Id;
        public DateOnly? Date => sichtung.Date;
        public string? Number => this.sichtung.VehicleNumber;
        public string? Location => this.sichtung.Location;
        public string? Context => this.sichtung.Context;
        public string? Note => this.sichtung.Note;

        public SichtungItemViewModel(SightingViewEntry sighting)
        {
            this.sichtung = sighting;
        }
    }
}
