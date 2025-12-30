using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.TabViewModels;
using Zugsichtungen.Wpf.ViewModels.DialogViewModels;

namespace Zugsichtungen.Wpf.ViewModels.TabViewModels
{
    public class SightingOverviewWpfTabViewModel : SightingOverviewTabViewModel
    {
        public SightingOverviewWpfTabViewModel(IDialogService dialogService,
            ILogger<SightingOverviewWpfTabViewModel> logger, 
            ISightingService sightingService,
            ISnackbarService snackbarService) : base(dialogService, logger, sightingService, snackbarService)
        {
        }

        protected override AddSichtungDialogViewModel CreateAddSichtungDialogViewModel()
        {
            return new AddSichtungWpfDialogViewModel(this.SightingService, this.DialogService);
        }

        protected override Task UpdateSightingsAsync()
        {
            return base.ReloadAllSightings();
        }
    }
}
