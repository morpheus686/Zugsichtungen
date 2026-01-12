using System.Collections.ObjectModel;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels;

namespace Zugsichtungen.ViewModels.ListBoxViewModels
{
    public class CheckedListViewModel<T, T2> : ViewModelBase where T : CheckedItemViewModelBase<T2>
    {
        public CheckedListViewModel()
        {
            Items = [];
        }

        public ObservableCollection<T> Items { get; }
        public bool HasCheckedItems => Items.Any(i => i.IsChecked);
    }
}
