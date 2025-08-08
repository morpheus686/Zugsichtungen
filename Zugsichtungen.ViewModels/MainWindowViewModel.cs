using AsyncAwaitBestPractices.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;
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
        private readonly ISightingService sichtungService;

        public ObservableCollection<SichtungItemViewModel> Sichtungsliste => this.sichtungenList; 
        public SichtungItemViewModel? SelectedItem { get; set; }

        public ICommand AddSichtungCommand { get; }
        public ICommand EditContextesCommand { get; }
        public ICommand ShowSightingDetailsCommand { get; }


        public MainWindowViewModel(IDialogService dialogService, ISightingService sichtungService)
        {
            AddSichtungCommand = new AsyncCommand(execute: ExecuteAddSichtung, canExecute: CanExecuteAddSichtung);
            EditContextesCommand = new AsyncCommand(execute: ExecuteEditContextes, canExecute: CanExecuteEditContextes);
            ShowSightingDetailsCommand = new AsyncCommand(execute: ExecuteShowSightingDetails, canExecute: CanExecuteShowSightingsDetails);

            this.sichtungenList = [];
            this.dialogService = dialogService;
            this.sichtungService = sichtungService;
        }

        private bool CanExecuteShowSightingsDetails(object? arg) => this.SelectedItem != null && !this.IsBusy;

        private async Task ExecuteShowSightingDetails()
        {
            IsBusy = true;
            var showDetailsViewModel = new ShowSightingDetailsDialogViewModel();
            await this.dialogService.ShowDialogAsync(showDetailsViewModel);
            IsBusy = false;
        }

        protected override Task InitializeInternalAsync()
        {
            return UpdateSichtungen();
        }

        private bool CanExecuteEditContextes(object? arg) => !this.IsBusy;

        private async Task ExecuteEditContextes()
        {
            await Task.CompletedTask;
        }

        private bool CanExecuteAddSichtung(object? parameter) => !this.IsBusy;

        private async Task ExecuteAddSichtung()
        {
            IsBusy = true;
            var addSichtungDialogViewModel = new AddSichtungDialogViewModel(sichtungService, dialogService);
            var result = await this.dialogService.ShowDialogAsync(addSichtungDialogViewModel);

            if (result == null)
            {
                throw new InvalidOperationException("Dialog konnte kein gültiges Ergebnis zurückliefern!");
            }

            if ((DialogResult)result == DialogResult.Yes)
            {
                await this.dialogService.ShowIndeterminateDialogAsync(async (updateMessage, parameter) =>
                {
                    updateMessage("Neue Sichtung wird gespeichert.");

                    await this.sichtungService.AddSichtungAsync(DateOnly.FromDateTime(addSichtungDialogViewModel.SelectedDate),
                        addSichtungDialogViewModel.SelectedFahrzeug.Id,
                        addSichtungDialogViewModel.SelectedKontext.Id,
                        addSichtungDialogViewModel.Place,
                        addSichtungDialogViewModel.Note,
                        addSichtungDialogViewModel.ImagePath);

                    await this.UpdateSichtungen();
                    updateMessage("Sichtung gespeichert.");
                    await Task.Delay(2000);
                });
            }

            IsBusy = false;
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
