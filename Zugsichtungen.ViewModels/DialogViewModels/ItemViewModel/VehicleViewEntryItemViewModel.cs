using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel
{
    public class VehicleViewEntryItemViewModel : ViewModelBase
    {
        private readonly VehicleViewEntry entry;

        public string Vehicle => entry.Vehicle;
        public int Id => entry.Id;

        public VehicleViewEntryItemViewModel(VehicleViewEntry entry)
        {
            this.entry = entry;
        }
    }
}
