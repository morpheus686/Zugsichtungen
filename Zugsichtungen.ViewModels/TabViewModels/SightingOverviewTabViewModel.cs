using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public class SightingOverviewTabViewModel : SightingOverviewTabViewModelBase
    {
        public SightingOverviewTabViewModel(IDialogService dialogService,
            ILogger<SightingOverviewTabViewModelBase> logger, 
            ISightingService sightingService,
            ISnackbarService snackbarService) : base(dialogService, logger, sightingService, snackbarService)
        {
        }

        protected override Task UpdateSightingsAsync()
        {
            return base.ReloadAllSightings();
        }
    }
}
