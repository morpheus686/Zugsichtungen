using System.Collections.ObjectModel;

namespace Zugsichtungen.Abstractions.Strategies
{
    public interface IUpdateStrategy<T>
    {
        Task Apply(ObservableCollection<T> collection, T item);
        Task Apply(ObservableCollection<T> collection, Action<T> addItem);
    }
}
