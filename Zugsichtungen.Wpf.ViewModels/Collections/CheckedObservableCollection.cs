using System.Collections.ObjectModel;
using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Wpf.ViewModels.Collections
{
    public class CheckedObservableCollection : ObservableCollection<ICheckable>
    {
        public bool HasCheckedItems()
        {
            foreach (var item in this)
            {
                if (item.IsChecked)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
