using System.Collections.ObjectModel;

namespace Zugsichtungen.Abstractions.Strategies
{
    public interface IUpdateStrategy<TViewModel, TItem>
    {
        void Apply(ObservableCollection<TViewModel> collection, TItem item);
        Task Apply(ObservableCollection<TViewModel> collection);
    }
}
