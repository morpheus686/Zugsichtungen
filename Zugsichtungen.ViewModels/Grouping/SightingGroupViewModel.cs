using System.Collections.ObjectModel;

namespace Zugsichtungen.ViewModels.Grouping
{
    public class SightingGroupViewModel : ObservableCollection<SichtungItemViewModel>
    {     
        public DateOnly? Date { get; }

        public SightingGroupViewModel(DateOnly? date, IEnumerable<SichtungItemViewModel> items) : base(items)
        {
            Date = date;
        }
    }
}
