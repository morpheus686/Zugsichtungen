using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.Enumerations;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class IndeterminateDialogViewModel : ViewModelBase
    {
        private IndeterminateState workingState;
        private string _message;

        public IndeterminateDialogViewModel() : this(String.Empty)
        {
        }

        public IndeterminateDialogViewModel(string message)
        {
            _message = message;
            this.workingState = IndeterminateState.Working;
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

        public IndeterminateState WorkingState
        {
            get => workingState;
            set 
            {
                if (this.workingState != value)
                {
                    workingState = value; 
                    RaisePropertyChanged(nameof(WorkingState));
                }
            }
        }

    }
}
