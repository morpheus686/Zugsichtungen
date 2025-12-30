using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.SignalR.Services;
using Zugsichtungen.ViewModels;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.Grouping;
using Zugsichtungen.ViewModels.TabViewModels;
using Zugsichtungen.Wpf.ViewModels.DialogViewModels;

namespace Zugsichtungen.SignalR.ViewModels.TabViewModels
{
    public class SightingOverviewSignalRTabViewModel : SightingOverviewTabViewModel
    {
        private readonly ISnackbarService snackbarService;

        public SightingOverviewSignalRTabViewModel(IDialogService dialogService,
            ILogger<SightingOverviewTabViewModel> logger,
            ISightingService sightingService,
            ISignalRClient signalRClient,
            ISnackbarService snackbarService) : base(dialogService, logger, sightingService, snackbarService)
        {
            signalRClient.On<SightingViewEntryDto>("SightingAdded", s => SightingAdded(s));
            this.snackbarService = snackbarService;
        }

        private void SightingAdded(SightingViewEntryDto s)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var itemViewModel = new SichtungItemViewModel(s, this.DialogService);
                this.Sichtungsliste.Add(itemViewModel);

                var group = this.GroupedSightings.FirstOrDefault(g => g.Number == s.VehicleNumber);

                if (group != null)
                {
                    group.Add(itemViewModel);
                }
                else
                {
                    var newGroup = new SightingGroupViewModel(s.VehicleNumber, [itemViewModel]);
                    this.GroupedSightings.Add(newGroup);
                }

                snackbarService.Show($"Neue Sichtung vom {s.Date} aus {s.Location} erhalten.");
            });
        }

        protected override Task UpdateSightingsAsync()
        {
            return Task.CompletedTask;
        }

        protected override AddSichtungDialogViewModel CreateAddSichtungDialogViewModel()
        {
            return new AddSichtungWpfDialogViewModel(this.SightingService, this.DialogService);
        }
    }
}
