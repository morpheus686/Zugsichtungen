using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Foundation.ViewModel
{
    public abstract class LoadableViewModel : ViewModelBase, ILoadable
    {
        private bool isInitializing = true;

        public bool IsInitializing
        {
            get => isInitializing;
            protected set
            {
                if (value != this.isInitializing)
                {
                    isInitializing = value;
                    RaisePropertyChanged(nameof(IsInitializing));
                }
            }
        }

        public async Task Initialize()
        {
            this.IsInitializing = true;
            InitializeInternal();
            await InitializeInternalAsync();
            this.IsInitializing = false;
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
