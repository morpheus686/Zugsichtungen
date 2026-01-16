using System.ComponentModel;
using System.Windows.Data;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel;

namespace Zugsichtungen.Wpf.ViewModels.DialogViewModels
{
    public class AddSichtungWpfDialogViewModel : AddSichtungDialogViewModel
    {
     public AddSichtungWpfDialogViewModel(ISightingService sightingService, 
         IDialogService dialogService)
            : base(sightingService, dialogService)
        {
            this.VehicleView = CollectionViewSource.GetDefaultView(this.VehicleList);
            this.VehicleView.Filter = FilterVehicles;
        }

        public ICollectionView VehicleView { get; }

        public override SeriesItemViewModel? SelectedSeries
        {
            get => base.SelectedSeries;
            set
            {
                base.SelectedSeries = value;
                VehicleView.Refresh();
                this.SelectedVehicle = VehicleView.Cast<VehicleItemViewModel>().FirstOrDefault();
                RaisePropertyChanged(nameof(SelectedVehicle));
            }
        }

        private bool FilterVehicles(object obj)
        {
            if (obj is not VehicleItemViewModel vehicle)
                return false;

            if (SelectedSeries is null)
                return true;

            return vehicle.SeriesId == SelectedSeries.Id;
        }
    }
}
