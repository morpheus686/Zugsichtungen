using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels
{
    public class VehicleCheckedItemViewModel : CheckedItemViewModelBase<VehicleDto>
    {
        public VehicleCheckedItemViewModel(VehicleDto item) : base(item)
        {
        }

        public override string? Text => Item.Number;
    }
}
