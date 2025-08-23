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
            private set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public IndeterminateState WorkingState
        {
            get => workingState;
            private set 
            {
                if (this.workingState != value)
                {
                    workingState = value; 
                    RaisePropertyChanged(nameof(WorkingState));
                }
            }
        }

        public void SetNewMessage(string message, IndeterminateState newIndeterminateState)
        {
            this.Message = message;
            this.WorkingState = newIndeterminateState;
        }
    }
}
