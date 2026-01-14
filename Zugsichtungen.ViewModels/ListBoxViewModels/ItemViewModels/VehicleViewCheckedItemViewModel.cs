using CommunityToolkit.Mvvm.ComponentModel;
using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels
{
    public class VehicleViewCheckedItemViewModel : CheckedItemViewModelBase<VehicleViewEntryDto>
    {
        public VehicleViewCheckedItemViewModel(VehicleViewEntryDto item) : base(item)
        {
        }

        public override string? Text => Item.Vehicle;
        public override int Id => Item.Id;
        public int SeriesId => Item.SeriesId;
    }
}
