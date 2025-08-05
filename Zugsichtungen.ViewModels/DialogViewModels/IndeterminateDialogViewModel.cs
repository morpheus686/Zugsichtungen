using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class IndeterminateDialogViewModel : ViewModelBase
    {
        private string _message;

        public IndeterminateDialogViewModel() : this(String.Empty)
        {
        }

        public IndeterminateDialogViewModel(string message)
        {
            _message = message;
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }
    }
}
