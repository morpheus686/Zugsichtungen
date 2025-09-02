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
    {        public SightingOverviewSignalRTabViewModel(IDialogService dialogService,
            ILogger<SightingOverviewTabViewModelBase> logger, 
            ISightingService sightingService,
            ISignalRClient signalRClient) : base(dialogService, logger, sightingService)
        {
            signalRClient.On<SightingViewEntryDto>("SightingAdded", s =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    this.Sichtungsliste.Add(new SichtungItemViewModel(s, dialogService));
                    bool groupFound = false;

                    foreach (var group in this.GroupedSightings)
                    {
                        if (group.Date == s.Date)
                        {
                            group.Add(new SichtungItemViewModel(s, dialogService));
                            groupFound = true;
                            break;
                        }
                    }

                    if (!groupFound)
                    {
                        var newGroup = new SightingGroupViewModel(s.Date, Enumerable.Empty<SichtungItemViewModel>())
                        {
                            new SichtungItemViewModel(s, dialogService)
                        };

                        this.GroupedSightings.Add(newGroup);
                    }
                });
            });
        }

        protected override Task UpdateSightings()
        {
            return Task.CompletedTask;
        }
    }
}
