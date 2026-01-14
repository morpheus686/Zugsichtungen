using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels
{
    public class VehicleCheckedItemViewModel : CheckedItemViewModelBase<VehicleDto>
    {
        public VehicleCheckedItemViewModel(VehicleDto item) : base(item)
        {
        }

        public override string? Text => Item.Number;

        public override int Id => Item.Id;
        public int? SeriesId => Item.SeriesId;
    }
}
