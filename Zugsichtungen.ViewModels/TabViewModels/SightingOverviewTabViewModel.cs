using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.Grouping;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public class SightingOverviewTabViewModel : SightingOverviewTabViewModelBase
    {
        private readonly IDialogService dialogService;
        private readonly ISightingService sightingService;

        public SightingOverviewTabViewModel(IDialogService dialogService, ILogger<SightingOverviewTabViewModelBase> logger, ISightingService sightingService) : base(dialogService, logger, sightingService)
        {
            this.dialogService = dialogService;
            this.sightingService = sightingService;
        }

        protected override Task UpdateSightings()
        {
            return base.ReloadAllSightings();
        }
    }
}
