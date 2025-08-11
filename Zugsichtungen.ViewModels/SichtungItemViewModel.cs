using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels
{
    public class SichtungItemViewModel : ViewModelBase
    {
        public SightingViewEntry Sichtung { get; }
        public SightingPicture? SightingPicture { get; }

        public int? Id => Sichtung.Id;
        public DateOnly? Date => Sichtung.Date;
        public string? Number => this.Sichtung.VehicleNumber;
        public string? Location => this.Sichtung.Location;
        public string? Context => this.Sichtung.Context;
        public string? Note => this.Sichtung.Note;
        public byte[]? Picture
        {
            get
            {
                if (this.SightingPicture == null)
                {
                    return null;
                }

                return this.SightingPicture.Image;
            }
        }

        public SichtungItemViewModel(SightingViewEntry sighting, SightingPicture? sightingPicture)
        {
            this.Sichtung = sighting;
            this.SightingPicture = sightingPicture;
        }
    }
}
