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
        public SightingOverviewSignalRTabViewModel(IDialogService dialogService,
            ILogger<SightingOverviewTabViewModelBase> logger,
            ISightingService sightingService,
            ISignalRClient signalRClient) : base(dialogService, logger, sightingService)
        {
            signalRClient.On<SightingViewEntryDto>("SightingAdded", s => SightingAdded(s));            
        }

        private void SightingAdded(SightingViewEntryDto s)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var itemViewModel = new SichtungItemViewModel(s, dialogService);
                this.Sichtungsliste.Add(itemViewModel);

                var group = this.GroupedSightings.FirstOrDefault(g => g.Date == s.Date);

                if (group != null)
                {
                    group.Add(itemViewModel);
                }
                else
                {
                    var newGroup = new SightingGroupViewModel(s.Date, [itemViewModel]);
                    this.GroupedSightings.Add(newGroup);
                }
            });
        }

        protected override Task UpdateSightings()
        {
            return Task.CompletedTask;
        }
    }
}
