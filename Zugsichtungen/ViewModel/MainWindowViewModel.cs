using AsyncAwaitBestPractices.MVVM;
using System.Collections.ObjectModel;
using Zugsichtungen.Components;
using Zugsichtungen.Services;
using Zugsichtungen.ViewModel.DialogViewModel;

namespace Zugsichtungen.ViewModel
{
    public class MainWindowViewModel : LoadableViewModel
    {
        private readonly ObservableCollection<SichtungItemViewModel> sichtungenList;
        private readonly IDialogService dialogService;
        private readonly ISichtungService sichtungService;
        private readonly IDataService dataService;

        public ObservableCollection<SichtungItemViewModel> Sichtungsliste => this.sichtungenList;

        public AsyncCommand AddSichtungCommand { get; }

        public MainWindowViewModel(IDialogService dialogService, ISichtungService sichtungService, IDataService dataService)
        {
            AddSichtungCommand = new AsyncCommand(execute: ExecuteAddSichtung, canExecute: CanExecuteAddSichtung);
            this.sichtungenList = [];

            this.dialogService = dialogService;
            this.sichtungService = sichtungService;
            this.dataService = dataService;
        }

        private bool CanExecuteAddSichtung(object? parameter) => !this.IsBusy;

        private async Task ExecuteAddSichtung()
        {
            IsBusy = true;
            var addSichtungDialogViewModel = new AddSichtungDialogViewModel(this.dataService);
            var result = await this.dialogService.ShowDialog(addSichtungDialogViewModel);

            if (result == null)
            {
                throw new InvalidOperationException("Dialog konnte kein gültiges Ergebnis zurückliefern!");
            }

            if ((DialogResult)result == DialogResult.Yes)
            {
                await this.sichtungService.AddSichtung(DateOnly.FromDateTime(addSichtungDialogViewModel.SelectedDate),
                    addSichtungDialogViewModel.SelectedFahrzeug.Id,
                    addSichtungDialogViewModel.SelectedKontext.Id,
                    addSichtungDialogViewModel.Place,
                    addSichtungDialogViewModel.Note);

                await this.UpdateSichtungen();
            }

            IsBusy = false;
        }

        protected override Task InitializeInternalAsync()
        {
            return UpdateSichtungen();
        }

        private async Task UpdateSichtungen()
        {
            this.Sichtungsliste.Clear();
            var sichtungen = await this.dataService.GetSichtungenAsync();

            foreach (var item in sichtungen)
            {
                Sichtungsliste.Add(new SichtungItemViewModel(item));
            }
        }
    }
}
