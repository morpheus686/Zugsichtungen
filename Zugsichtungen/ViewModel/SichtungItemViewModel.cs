using Zugsichtungen.Models;

namespace Zugsichtungen.ViewModel
{
    public class SichtungItemViewModel : ViewModelBase
    {
        private readonly Sichtungen sichtung;

        public int Id => sichtung.Id;

        public DateOnly? Date => sichtung.Datum;

        public string Number
        {
            get
            {
                if (this.sichtung.Fahrzeug == null)
                {
                    return "---";
                }

                if (this.sichtung.Fahrzeug.Baureihe == null)
                {
                    return "---";
                }

                return this.sichtung.Fahrzeug.Baureihe.Nummer + " " + this.sichtung.Fahrzeug.Nummer;
            }
        }

        public string? Location => this.sichtung.Ort;
        public string Context
        {
            get
            {
                if (this.sichtung.Kontext == null)
                {
                    return "---";
                }

                return this.sichtung.Kontext.Name;
            }
        }

        public string? Note => this.sichtung.Bemerkung;

        public SichtungItemViewModel(Sichtungen sichtung)
        {
            this.sichtung = sichtung;
        }
    }
}
