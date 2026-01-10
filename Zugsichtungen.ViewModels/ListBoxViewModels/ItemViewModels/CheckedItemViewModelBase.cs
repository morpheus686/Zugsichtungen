using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.ListBoxViewModels.ItemViewModels
{
    public abstract class CheckedItemViewModelBase<T> : ViewModelBase
    {
        protected CheckedItemViewModelBase(T item)
        {
            Item = item;
        }

        public event EventHandler<EventArgs>? CheckedChanged;

        private bool _isChecked;

        public abstract string? Text { get; }
        public T Item { get; }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged(nameof(IsChecked));
                    CheckedChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
