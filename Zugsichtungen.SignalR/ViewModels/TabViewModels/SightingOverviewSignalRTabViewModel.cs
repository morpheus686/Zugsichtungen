using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.SignalR.Services;
using Zugsichtungen.ViewModels;
using Zugsichtungen.ViewModels.Grouping;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.SignalR.ViewModels.TabViewModels
{
    public class SightingOverviewSignalRTabViewModel : SightingOverviewTabViewModelBase
    {
        private readonly ISnackbarService snackbarService;

        public SightingOverviewSignalRTabViewModel(IDialogService dialogService,
            ILogger<SightingOverviewTabViewModelBase> logger,
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
                var itemViewModel = new SichtungItemViewModel(s, dialogService);
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
    }
}
