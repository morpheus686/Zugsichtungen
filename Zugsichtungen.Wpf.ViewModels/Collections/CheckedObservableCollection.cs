using System.Collections.ObjectModel;
using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Wpf.ViewModels.Collections
{
    public class CheckedObservableCollection : ObservableCollection<ICheckable>
    {
        public bool HasCheckedItems => this.Any(item => item.IsChecked);
    }
}
