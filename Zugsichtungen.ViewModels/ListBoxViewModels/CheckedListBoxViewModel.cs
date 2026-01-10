using System.Collections.ObjectModel;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels;

namespace Zugsichtungen.ViewModels.ListBoxViewModels
{
    public class CheckedListBoxViewModel<T> : ViewModelBase
    {
        public CheckedListBoxViewModel()
        {
            Items = [];
        }

        public ObservableCollection<CheckedItemViewModelBase<T>> Items { get; }
    }
}
