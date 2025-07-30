using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Zugsichtungen.Foundation.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    RaisePropertyChanged(nameof(IsBusy));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
