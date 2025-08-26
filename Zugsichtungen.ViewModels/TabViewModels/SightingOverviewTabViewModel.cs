using AsyncAwaitBestPractices.MVVM;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.Enumerations;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.Enumerations;
using Zugsichtungen.ViewModels.Grouping;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public class SightingOverviewTabViewModel : TabViewModelBase
    {
        private readonly ObservableCollection<SichtungItemViewModel> sichtungenList;
        private readonly IDialogService dialogService;
        private readonly ILogger<SightingOverviewTabViewModel> logger;
        private readonly ISightingService sightingService;

        public ObservableCollection<SichtungItemViewModel> Sichtungsliste => this.sichtungenList;
        public ObservableCollection<SightingGroupViewModel> GroupedSightings { get; }
        public SichtungItemViewModel? SelectedItem { get; set; }

        public ICommand AddSichtungCommand { get; }
        public ICommand EditContextesCommand { get; }
        public ICommand ShowSightingDetailsCommand { get; }

        public SightingOverviewTabViewModel(IDialogService dialogService,
            IDataService dataService,
            ILogger<SightingOverviewTabViewModel> logger,
            ISightingService sightingService)
        {
            this.Title = "Sichtungen";

            AddSichtungCommand = new AsyncCommand(execute: ExecuteAddSichtung, canExecute: CanExecuteAddSichtung);
            EditContextesCommand = new AsyncCommand(execute: ExecuteEditContextes, canExecute: CanExecuteEditContextes);
            ShowSightingDetailsCommand = new AsyncCommand(execute: ExecuteShowSightingDetails, canExecute: CanExecuteShowSightingsDetails);

            this.sichtungenList = [];
            this.GroupedSightings = [];
            this.dialogService = dialogService;
            this.logger = logger;
            this.sightingService = sightingService;
        }

        private bool CanExecuteShowSightingsDetails(object? arg) => this.SelectedItem != null && !this.IsBusy;

        private async Task ExecuteShowSightingDetails()
        {
            IsBusy = true;

            if (this.SelectedItem != null)
            {
                var showDetailsViewModel = new ShowSightingDetailsDialogViewModel(this.sightingService, this.SelectedItem.Sichtung, this.dialogService);
                await this.dialogService.ShowDialogAsync(showDetailsViewModel);
            }


            IsBusy = false;
        }

        protected async override Task InitializeInternalAsync()
        {
            try
            {
                await UpdateSichtungen();
                this.IsInitializing = false;
                RaisePropertyChanged(nameof(this.IsInitializing));
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
            var addSichtungDialogViewModel = new AddSichtungDialogViewModel(this.sightingService, this.dialogService);
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

                    var newSightingDto = new SightingDto
                    {
                        VehicleId = addSichtungDialogViewModel.SelectedFahrzeug.Id,
                        ContextId = addSichtungDialogViewModel.SelectedKontext.Id,
                        Location = addSichtungDialogViewModel.Place,
                        Date = DateOnly.FromDateTime(addSichtungDialogViewModel.SelectedDate),
                        Note = addSichtungDialogViewModel.Note
                    };

                    var filePath = addSichtungDialogViewModel.ImagePath;
                    SightingPictureDto? sightingPictureDto = null;

                    if (filePath != null)
                    {
                        var picture = await File.ReadAllBytesAsync(filePath);

                        sightingPictureDto = new SightingPictureDto
                        {
                            Filename = new FileInfo(filePath).Name,
                            Image = picture,
                            Thumbnail = ImageHelper.CreateThumbnail(picture)
                        };
                    }

                    await this.sightingService.AddSightingAsync(newSightingDto, sightingPictureDto);

                });

                await this.UpdateSichtungen();
            }

            IsBusy = false;
        }

        private async Task UpdateSichtungen()
        {
            this.Sichtungsliste.Clear();
            var sichtungen = await this.sightingService.GetAllSightingViewEntriesAsync();

            foreach (var item in sichtungen)
            {
                Sichtungsliste.Add(new SichtungItemViewModel(item, this.dialogService));
            }

            var groups = Sichtungsliste
                .GroupBy(x => x.Date)
                .Select(g => new SightingGroupViewModel(g.Key, g))
                .OrderByDescending(g => g.Date);

            GroupedSightings.Clear();

            foreach (var group in groups)
            {
                GroupedSightings.Add(group);
            }
        }
    }
}