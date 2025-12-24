using System.Collections.ObjectModel;

namespace Zugsichtungen.ViewModels.Grouping
{
    public class SightingGroupViewModel : ObservableCollection<SichtungItemViewModel>
    {     
        public string? Number { get; }

        public SightingGroupViewModel(string? number, IEnumerable<SichtungItemViewModel> items) : base(items)
        {
            Number = number;
        }
    }
}
