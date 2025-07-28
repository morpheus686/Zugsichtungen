using AsyncAwaitBestPractices.MVVM;
using System.Collections.ObjectModel;
using Zugsichtungen.Models;
using Zugsichtungen.Services;
using Zugsichtungen.ViewModel.DialogViewModel;

namespace Zugsichtungen.ViewModel
{
    public class MainWindowViewModel : LoadableViewModel
    {
        private readonly ObservableCollection<SichtungItemViewModel> sichtungenList;
        private readonly IDialogService dialogService;
        private readonly IDataService dataService;
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

        public MainWindowViewModel(IDialogService dialogService, IDataService dataService)
        {
            AddSichtungCommand = new AsyncCommand(execute: ExecuteAddSichtung, canExecute: CanExecuteAddSichtung);
            this.sichtungenList = new ObservableCollection<SichtungItemViewModel>();

            this.dialogService = dialogService;
            this.dataService = dataService;
        }

        private bool CanExecuteAddSichtung(object? parameter) => !this.isBusy;

        private async Task ExecuteAddSichtung()
        {
            IsBusy = true;
            var addSichtungDialogViewModel = new AddSichtungDialogViewModel(this.dataService);
            var result = await this.dialogService.ShowDialog(addSichtungDialogViewModel);
            IsBusy = false;
        }

        protected async override Task InitializeInternalAsync()
        {
            var sichtungen = await this.dataService.GetSichtungenAsync();

            foreach (var item in sichtungen)
            {
                Sichtungsliste.Add(new SichtungItemViewModel(item));
            }
        }
    }
}
