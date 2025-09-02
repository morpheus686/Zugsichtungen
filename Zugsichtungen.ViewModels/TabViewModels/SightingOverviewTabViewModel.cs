using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public class SightingOverviewTabViewModel : SightingOverviewTabViewModelBase
    {
        public SightingOverviewTabViewModel(IDialogService dialogService, ILogger<SightingOverviewTabViewModelBase> logger, ISightingService sightingService) : base(dialogService, logger, sightingService)
        {
        }

        protected override Task UpdateSightingsAsync()
        {
            return base.ReloadAllSightings();
        }
    }
}
