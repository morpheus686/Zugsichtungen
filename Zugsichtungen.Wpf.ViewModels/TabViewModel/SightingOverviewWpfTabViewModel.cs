using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels;
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
            this.SeriesFilterList = [];
            this.VehicleViewFilterList = [];

            this.SightingsView = CollectionViewSource.GetDefaultView(this.Sichtungsliste);
            this.SightingsView.Filter = FilterSightings;
            this.SightingsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(SichtungItemViewModel.Number)));
            this.SightingsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(SichtungItemViewModel.Date)));

            this.VehicleView = CollectionViewSource.GetDefaultView(this.VehicleViewFilterList);
            this.VehicleView.Filter = FilterVehicles;

            this.SeriesView = CollectionViewSource.GetDefaultView(this.SeriesFilterList);
            this.SeriesView.SortDescriptions.Add(new SortDescription(nameof(SeriesCheckedItemViewModel.Text), ListSortDirection.Ascending));
        }

        private bool FilterVehicles(object obj)
        {
            return true;
        }

        public ICollectionView SightingsView { get; }
        public ICollectionView SeriesView { get; }
        public ICollectionView VehicleView { get; }
        public ObservableCollection<SeriesCheckedItemViewModel> SeriesFilterList { get; }
        public ObservableCollection<VehicleViewCheckedItemViewModel> VehicleViewFilterList { get; }

        private bool FilterSightings(object obj)
        {
            return true;
        }

        protected override AddSichtungDialogViewModel CreateAddSichtungDialogViewModel()
        {
            return new AddSichtungWpfDialogViewModel(this.SightingService, this.DialogService);
        }

        protected override Task UpdateSightingsAsync()
        {
            return base.ReloadAllSightings();
        }

        protected override async Task InitializeInternalAsync()
        {
            await base.InitializeInternalAsync();

            await LoadFilterAsync(
                this.SightingService.GetAllSeriesAsync,
                this.SeriesFilterList,
                dto => new SeriesCheckedItemViewModel(dto),
                this.VehicleView.Refresh);

            await LoadFilterAsync(
                this.SightingService.GetVehicleViewEntriesAsync,
                this.VehicleViewFilterList,
                dto => new VehicleViewCheckedItemViewModel(dto),
                this.SightingsView.Refresh);
        }

        private async Task LoadFilterAsync<TDto, TItemViewModel>(
            Func<Task<List<TDto>>> loadFunc,
            ObservableCollection<TItemViewModel> targetCollection,
            Func<TDto, TItemViewModel> createViewModel,
            Action onCheckedChanged)
            where TItemViewModel : CheckedItemViewModelBase<TDto>
        {
            var items = await loadFunc();

            foreach (var dto in items)
            {
                var item = createViewModel(dto);
                item.CheckedChanged += (_, _) => onCheckedChanged();
                targetCollection.Add(item);
            }
        }
    }
}
