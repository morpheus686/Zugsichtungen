using AsyncAwaitBestPractices.MVVM;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Zugsichtungen.Models;
using Zugsichtungen.ViewModel.DialogViewModel;

namespace Zugsichtungen.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ZugbeobachtungenContext context;
        private ObservableCollection<SichtungItemViewModel> sichtungenList;
        private readonly DialogHostManager dialogHostManager;
        private bool isBusy = false;

        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    RaisePropertyChanged(nameof(IsBusy));
                }
            }
        }

        public ObservableCollection<SichtungItemViewModel> Sichtungsliste
        {
            get => this.sichtungenList;
        }

        public AsyncCommand AddSichtungCommand { get; }

        public MainWindowViewModel()
        {
            AddSichtungCommand = new AsyncCommand(execute: ExecuteAddSichtung, canExecute: CanExecuteAddSichtung);

            this.sichtungenList = new ObservableCollection<SichtungItemViewModel>();
            this.context = new ZugbeobachtungenContext();
            this.dialogHostManager = new DialogHostManager();
        }

        private bool CanExecuteAddSichtung(object? parameter) => !this.isBusy;

        private async Task ExecuteAddSichtung()
        {
            IsBusy = true;
            var addSichtungDialogViewModel = new AddSichtungDialogViewModel();
            var result = await this.dialogHostManager.ShowDialog(addSichtungDialogViewModel);
            IsBusy = false;
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
