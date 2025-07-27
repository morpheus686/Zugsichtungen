using System.ComponentModel;

namespace Zugsichtungen.ViewModel.DialogViewModel
{
    public class AddSichtungDialogViewModel : LoadableViewModel, IDataErrorInfo
    {
        public AddSichtungDialogViewModel()
        {
            SelectedDate = DateTime.Now;
        }

        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { 
                selectedDate = value;
                RaisePropertyChanged(nameof(selectedDate));
            }
        }


        public string this[string columnName]
        {
            get 
            {
                if (columnName == nameof(SelectedDate))
                {

                }

                return string.Empty; 
            }
        }

        public string Error => null;

        protected override Task InitializeInternalAsync()
        {
            return base.InitializeInternalAsync();
        }
    }
}
