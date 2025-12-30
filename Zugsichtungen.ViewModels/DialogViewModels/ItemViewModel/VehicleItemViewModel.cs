using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel
{
    public class VehicleItemViewModel : ViewModelBase
    {
        private readonly VehicleDto vehicle;

        public int Id => this.vehicle.Id;
        public string? Number => this.vehicle.Number;
        public int? SeriesId => this.vehicle.SeriesId;

        public VehicleItemViewModel(VehicleDto vehicleDto)
        {
            this.vehicle = vehicleDto;
        }
    }
}
