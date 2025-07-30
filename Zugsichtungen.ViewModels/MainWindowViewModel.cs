using AsyncAwaitBestPractices.MVVM;
using System.Collections.ObjectModel;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.Enumerations;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.DialogViewModels;

namespace Zugsichtungen.ViewModels
{
    public class MainWindowViewModel : LoadableViewModel
    {
        private readonly ObservableCollection<SichtungItemViewModel> sichtungenList;
        private readonly IDialogService dialogService;
        private readonly ISichtungService sichtungService;

        public ObservableCollection<SichtungItemViewModel> Sichtungsliste => this.sichtungenList;

        public AsyncCommand AddSichtungCommand { get; }

        public MainWindowViewModel(IDialogService dialogService, ISichtungService sichtungService)
        {
            AddSichtungCommand = new AsyncCommand(execute: ExecuteAddSichtung, canExecute: CanExecuteAddSichtung);
            this.sichtungenList = [];

            this.dialogService = dialogService;
            this.sichtungService = sichtungService;
        }

        private bool CanExecuteAddSichtung(object? parameter) => !this.IsBusy;

        private async Task ExecuteAddSichtung()
        {
            IsBusy = true;
            var addSichtungDialogViewModel = new AddSichtungDialogViewModel(sichtungService);
            var result = await this.dialogService.ShowDialog(addSichtungDialogViewModel);

            if (result == null)
            {
                throw new InvalidOperationException("Dialog konnte kein gültiges Ergebnis zurückliefern!");
            }

            if ((DialogResult)result == DialogResult.Yes)
            {
                await this.sichtungService.AddSichtungAsync(DateOnly.FromDateTime(addSichtungDialogViewModel.SelectedDate),
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
            var sichtungen = await this.sichtungService.GetAllSightingsAsync();

            foreach (var item in sichtungen)
            {
                Sichtungsliste.Add(new SichtungItemViewModel(item));
            }
        }
    }
}
