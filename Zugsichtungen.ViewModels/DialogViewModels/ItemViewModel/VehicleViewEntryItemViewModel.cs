using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel
{
    public class VehicleViewEntryItemViewModel : ViewModelBase
    {
        private readonly VehicleViewEntryDto entry;

        public string Vehicle => entry.Vehicle;
        public int Id => entry.Id;

        public VehicleViewEntryItemViewModel(VehicleViewEntryDto entry)
        {
            this.entry = entry;
        }
    }
}
