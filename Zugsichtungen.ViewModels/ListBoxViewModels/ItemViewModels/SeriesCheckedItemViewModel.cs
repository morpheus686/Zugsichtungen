
using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels
{
    public class SeriesCheckedItemViewModel : CheckedItemViewModelBase<SeriesDto>
    {
        public SeriesCheckedItemViewModel(SeriesDto item) : base(item)
        {
        }

        public override string? Text => Item.Number;
    }
}
