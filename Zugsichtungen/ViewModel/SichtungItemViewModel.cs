using Zugsichtungen.Models;

namespace Zugsichtungen.ViewModel
{
    public class SichtungItemViewModel : ViewModelBase
    {
        private readonly Sichtungsview sichtung;

        public int? Id => sichtung.Id;
        public DateOnly? Date => sichtung.Datum;
        public string? Number => this.sichtung.Loknummer;
        public string? Location => this.sichtung.Ort;
        public string? Context => this.sichtung.Thema;
        public string? Note => this.sichtung.Bemerkung;

        public SichtungItemViewModel(Sichtungsview sichtung)
        {
            this.sichtung = sichtung;
        }
    }
}
