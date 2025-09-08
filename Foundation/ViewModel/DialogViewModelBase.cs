using System.Collections;
using System.ComponentModel;

namespace Zugsichtungen.Foundation.ViewModel
{
    public abstract class DialogViewModelBase : LoadableViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors;
        private string? title;

        protected DialogViewModelBase()
        {
            _errors = new Dictionary<string, List<string>>();
        }
        
        public string? Title
        {
            get { return title; }
            set 
            { 
                if (title == value) return;
                title = value; 
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public virtual bool HasErrors => _errors.Any();

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors.SelectMany(err => err.Value);
            }

            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }

            return Enumerable.Empty<string>();
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            if (!_errors[propertyName].Contains(errorMessage))
            {
                _errors[propertyName].Add(errorMessage);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
