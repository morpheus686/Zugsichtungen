using System.Collections.ObjectModel;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Abstractions.Strategies;

namespace Zugsichtungen.ApplicationBase.Strategies
{
    public class SightingReloadStrategy : IUpdateStrategy<SightingViewEntryDto>
    {
        private readonly ISightingService sightingService;

        public SightingReloadStrategy(ISightingService sightingService)
        {
            this.sightingService = sightingService;
        }

        public Task Apply(ObservableCollection<SightingViewEntryDto> collection, SightingViewEntryDto item)
        {
            throw new NotImplementedException();
        }

        public async Task Apply(ObservableCollection<SightingViewEntryDto> collection, Action<SightingViewEntryDto> addItem)
        {
            collection.Clear();
            var sichtungen = await this.sightingService.GetAllSightingViewEntriesAsync();

            foreach (var item in sichtungen)
            {
                addItem(item);
            }
        }
    }
}
