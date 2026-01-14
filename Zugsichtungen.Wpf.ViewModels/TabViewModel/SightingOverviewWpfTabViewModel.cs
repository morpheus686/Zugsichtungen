using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.ListBoxViewModels;
using Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels;
using Zugsichtungen.ViewModels.TabViewModels;
using Zugsichtungen.Wpf.ViewModels.Collections;
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
            //this.SeriesFilterList = [];
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
            if (obj is not VehicleViewCheckedItemViewModel vehicleViewItem)
            {
                return false;
            }

            foreach (var item in this.SeriesFilterList)
            {
                if (item.Id == vehicleViewItem.SeriesId
                    && item.IsChecked)
                {
                    return true;
                }
            }

            return false;
        }

        public ICollectionView SightingsView { get; }
        public ICollectionView SeriesView { get; }
        public ICollectionView VehicleView { get; }

        public CheckedObservableCollection SeriesFilterList { get; }
        public CheckedObservableCollection VehicleViewFilterList { get; }

        private bool FilterSightings(object obj)
        {
            if (obj is not SichtungItemViewModel itemViewModel)
            {
                return false;
            }

            foreach (var item in this.VehicleViewFilterList)
            {
                if (item.Id == itemViewModel.VehicleId
                    && item.IsChecked)
                {
                    return true;
                }
            }

            return false;
        }

        protected override AddSichtungDialogViewModel CreateAddSichtungDialogViewModel()
        {
            return new AddSichtungWpfDialogViewModel(this.SightingService, this.DialogService);
        }

        protected override async Task UpdateSightingsAsync()
        {
            await base.ReloadAllSightings();
            this.SightingsView.Refresh();
        }

        protected override async Task InitializeInternalAsync()
        {    
            await LoadFilterAsync(
                this.SightingService.GetAllSeriesAsync,
                this.SeriesFilterList,
                dto => new SeriesCheckedItemViewModel(dto),
                OnSeriesCheckedChanged);

            await LoadFilterAsync(
                this.SightingService.GetVehicleViewEntriesAsync,
                this.VehicleViewFilterList,
                dto => new VehicleViewCheckedItemViewModel(dto),
                OnVehicleCheckedChanged);

            await base.InitializeInternalAsync();
        }

        private async Task LoadFilterAsync<TDto, TItemViewModel>(
            Func<Task<List<TDto>>> loadFunc,
            CheckedObservableCollection targetCollection,
            Func<TDto, TItemViewModel> createViewModel,
            EventHandler<EventArgs> onCheckedChanged)
            where TItemViewModel : CheckedItemViewModelBase<TDto>
        {
            var items = await loadFunc();

            foreach (var dto in items)
            {
                var item = createViewModel(dto);
                item.CheckedChanged += onCheckedChanged;
                targetCollection.Add(item);                
            }
        }

        private void OnSeriesCheckedChanged(object? sender, EventArgs args)
        {
            if (sender is SeriesCheckedItemViewModel seriesCheckedItem
                && !seriesCheckedItem.IsChecked)
            {
                foreach (var vehicle in VehicleViewFilterList.Select(i => i as VehicleViewCheckedItemViewModel))
                {
                    if (vehicle == null)
                    {
                        continue;
                    }

                    if (vehicle.SeriesId == seriesCheckedItem.Id && vehicle.IsChecked)
                    {
                        vehicle.IsChecked = false;
                    }
                }
            }

            this.VehicleView.Refresh();
            this.SightingsView.Refresh();
        }

        private void OnVehicleCheckedChanged(object? sender, EventArgs args)
        {
            this.SightingsView.Refresh();
        }
    }
}
