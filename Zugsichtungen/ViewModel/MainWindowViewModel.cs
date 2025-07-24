using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Zugsichtungen.Models;

namespace Zugsichtungen.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ZugbeobachtungenContext context;
        private ObservableCollection<SichtungItemViewModel> sichtungenList;

        public ObservableCollection<SichtungItemViewModel> Sichtungsliste
        {
            get => this.sichtungenList;
        }

        public MainWindowViewModel()
        { 
            this.sichtungenList = new ObservableCollection<SichtungItemViewModel>();
            this.context = new ZugbeobachtungenContext();
        }

        public void Initialize()
        {
            var sichtungen = this.context.Sichtungens
                .Include(e => e.Fahrzeug)
                .ThenInclude(e => e.Baureihe)
                .Include(e => e.Kontext)
                .OrderBy(e => e.Datum);

            foreach (var item in sichtungen)
            {
                if (item == null)
                {
                    continue;
                }

                Sichtungsliste.Add(new SichtungItemViewModel(item));
            }
        }
    }
}
