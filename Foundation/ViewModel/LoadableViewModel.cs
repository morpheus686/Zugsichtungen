using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Foundation.ViewModel
{
    public abstract class LoadableViewModel : ViewModelBase, ILoadable
    {
        public bool IsInitializing { get; protected set; } = true;

        public async Task Initialize()
        {
            InitializeInternal();
            await InitializeInternalAsync();
            this.IsInitializing = false;
            RaisePropertyChanged(nameof(IsInitializing));
        }

        protected virtual void InitializeInternal()
        {
        }

        protected virtual Task InitializeInternalAsync()
        {
            return Task.CompletedTask;
        }
    }
}
