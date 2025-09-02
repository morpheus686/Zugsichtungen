using System.Collections.ObjectModel;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Abstractions.Strategies;
using Zugsichtungen.ViewModels;

namespace Zugsichtungen.ApplicationBase.Strategies
{
    public class SightingReloadStrategy : IUpdateStrategy<SichtungItemViewModel, SightingViewEntryDto>
    {
        private readonly ISightingService sightingService;
        private readonly IDialogService dialogService;

        public SightingReloadStrategy(ISightingService sightingService, IDialogService dialogService)
        {
            this.sightingService = sightingService;
            this.dialogService = dialogService;
        }

        public void Apply(ObservableCollection<SichtungItemViewModel> collection, SightingViewEntryDto item)
        {
            collection.Add(new SichtungItemViewModel(item, dialogService));
        }

        public async Task Apply(ObservableCollection<SichtungItemViewModel> collection)
        {
            collection.Clear();
            var viewEntries = await this.sightingService.GetAllSightingViewEntriesAsync();

            foreach (var item in viewEntries)
            {
                collection.Add(new SichtungItemViewModel(item, dialogService));
            }
        }
    }
}
