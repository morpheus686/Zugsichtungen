using AsyncAwaitBestPractices.MVVM;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.Enumerations;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.Enumerations;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public class SightingOverviewTabViewModel : TabViewModelBase
    {
        private readonly ObservableCollection<SichtungItemViewModel> sichtungenList;
        private readonly IDialogService dialogService;
        private readonly ISightingService sichtungService;
        private readonly ILogger<SightingOverviewTabViewModel> logger;

        public ObservableCollection<SichtungItemViewModel> Sichtungsliste => this.sichtungenList;
        public SichtungItemViewModel? SelectedItem { get; set; }

        public ICommand AddSichtungCommand { get; }
        public ICommand EditContextesCommand { get; }
        public ICommand ShowSightingDetailsCommand { get; }


        public SightingOverviewTabViewModel(IDialogService dialogService, ISightingService sichtungService, ILogger<SightingOverviewTabViewModel> logger)
        {
            AddSichtungCommand = new AsyncCommand(execute: ExecuteAddSichtung, canExecute: CanExecuteAddSichtung);
            EditContextesCommand = new AsyncCommand(execute: ExecuteEditContextes, canExecute: CanExecuteEditContextes);
            ShowSightingDetailsCommand = new AsyncCommand(execute: ExecuteShowSightingDetails, canExecute: CanExecuteShowSightingsDetails);

            this.sichtungenList = [];
            this.dialogService = dialogService;
            this.sichtungService = sichtungService;
            this.logger = logger;
        }

        private bool CanExecuteShowSightingsDetails(object? arg) => this.SelectedItem != null && !this.IsBusy;

        private async Task ExecuteShowSightingDetails()
        {
            IsBusy = true;

            if (this.SelectedItem != null)
            {
                var showDetailsViewModel = new ShowSightingDetailsDialogViewModel(sichtungService, this.SelectedItem.Sichtung, this.dialogService);
                await this.dialogService.ShowDialogAsync(showDetailsViewModel);
            }


            IsBusy = false;
        }

        protected override Task InitializeInternalAsync()
        {
            try
            {
                return UpdateSichtungen();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
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
                    updateMessage("Neue Sichtung wird gespeichert.", IndeterminateState.Working);

                    await this.sichtungService.AddSichtungAsync(DateOnly.FromDateTime(addSichtungDialogViewModel.SelectedDate),
                        addSichtungDialogViewModel.SelectedFahrzeug.Id,
                        addSichtungDialogViewModel.SelectedKontext.Id,
                        addSichtungDialogViewModel.Place,
                        addSichtungDialogViewModel.Note,
                        addSichtungDialogViewModel.ImagePath);
                    await this.UpdateSichtungen();
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
                var pictureExists = await this.sichtungService.CheckIfPictureExists(item.Id);
                SightingPicture? sightingPicture = null;

                if (pictureExists)
                {
                    sightingPicture = await this.sichtungService.GetSightingPictureByIdAsync(item.Id);
                }

                Sichtungsliste.Add(new SichtungItemViewModel(item, this.dialogService, sightingPicture));
            }
        }
    }
}