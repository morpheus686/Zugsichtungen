using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.SignalR.Services;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.SignalR.ViewModels.TabViewModels
{
    public class SightingOverviewSignalRTabViewModel : SightingOverviewTabViewModelBase
    {
        public SightingOverviewSignalRTabViewModel(IDialogService dialogService,
            ILogger<SightingOverviewTabViewModelBase> logger, 
            ISightingService sightingService,
            ISignalRClient signalRClient) : base(dialogService, logger, sightingService)
        {
            signalRClient.On<SightingViewEntryDto>("SightingAdded", s =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    this.Sichtungsliste.Add(new Zugsichtungen.ViewModels.SichtungItemViewModel(s, dialogService));
                });
            });
        }

        protected override Task UpdateSightings()
        {
            return Task.CompletedTask;
        }
    }
}
