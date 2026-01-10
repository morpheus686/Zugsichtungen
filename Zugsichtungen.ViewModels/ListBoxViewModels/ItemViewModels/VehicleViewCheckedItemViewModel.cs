using System;
using System.Collections.Generic;
using System.Text;
using Zugsichtungen.Abstractions.DTO;

namespace Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels
{
    public class VehicleViewCheckedItemViewModel : CheckedItemViewModelBase<VehicleViewEntryDto>
    {
        public VehicleViewCheckedItemViewModel(VehicleViewEntryDto item) : base(item)
        {
        }

        public override string? Text => Item.Vehicle;
    }
}
