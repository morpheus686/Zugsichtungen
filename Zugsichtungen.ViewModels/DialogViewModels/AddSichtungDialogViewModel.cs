using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class AddSichtungDialogViewModel : DialogViewModelBase, INotifyDataErrorInfo
    {
        public AddSichtungDialogViewModel(ISightingService sichtungService, IDialogService dialogService)
        {
            SelectedDate = DateTime.Now;
            this.sichtungService = sichtungService;
            this.dialogService = dialogService;
            this.VehicleList = [];
            this.ContextList = [];
            _errors = new Dictionary<string, List<string>>();

            this.AddImageCommand = new AsyncRelayCommand(ExececuteAddImageCommand);
            this.RemoveImageCommand = new RelayCommand(ExecuteRemoveImageCommand);
        }

        private DateTime selectedDate;
        private string? imagePath = null;
        private string place = string.Empty;
        private readonly ISightingService sichtungService;
        private readonly IDialogService dialogService;
        private readonly Dictionary<string, List<string>> _errors;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public VehicleViewEntry SelectedFahrzeug { get; set; } = null!;
        public Context SelectedKontext { get; set; } = null!;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set 
            { 
                selectedDate = value;
                RaisePropertyChanged(nameof(selectedDate));
            }
        }

        public ObservableCollection<VehicleViewEntry> VehicleList { get; private set; }
        public ObservableCollection<Context> ContextList { get; private set; }

        public string Note { get; set; } = string.Empty;
        public string Place
        {
            get => place;
            set
            {
                if (place != value)
                {
                    place = value;
                    ValidatePlace();
                    RaisePropertyChanged(nameof(Place));
                    RaisePropertyChanged(nameof(HasErrors));
                }
            }
        }
        public string? ImagePath
        {
            get => imagePath;
            private set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    RaisePropertyChanged(nameof(imagePath));
                }                
            }
        }
        public ICommand AddImageCommand { get; }
        public ICommand RemoveImageCommand { get; }

        public bool PlaceIsInvalid { get; private set; }
        public  bool HasErrors => _errors.Any();

        protected override async Task InitializeInternalAsync()
        {
            await LoadAndSelectFirstAsync(
                this.sichtungService.GetAllVehicleViewEntriesAsync,
                this.VehicleList,
                item => this.SelectedFahrzeug = item,
                nameof(SelectedFahrzeug));

            await LoadAndSelectFirstAsync(
                this.sichtungService.GetAllContextesAsync,
                this.ContextList,
                item => this.SelectedKontext = item,
                nameof(SelectedKontext));

            ValidatePlace();
        }

        private async Task LoadAndSelectFirstAsync<T>(
            Func<Task<List<T>>> loadFunc,
            ObservableCollection<T> targetCollection,
            Action<T> setSelectedItem,
            string selectedPropertyName)
        {
            var items = await loadFunc();
            targetCollection.Clear();

            foreach (var item in items)
            {
                targetCollection.Add(item);
            }

            if (targetCollection.Any())
            {
                setSelectedItem(targetCollection.First());
                RaisePropertyChanged(selectedPropertyName);
            }
        }

        private async Task ExececuteAddImageCommand()
        {
            var result = await this.dialogService.ShowOpenFileDialogAsync();

            if (result == null)
            {
                return;
            }

            this.ImagePath = result;
        }

        private void ExecuteRemoveImageCommand()
        {
            this.ImagePath = null;
        }

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

        private void ValidatePlace()
        {
            const string propertyName = nameof(Place);

            if (string.IsNullOrWhiteSpace(Place))
            {
                AddError(propertyName, "Der Ort darf nicht leer sein!");
            }
            else
            {
                ClearErrors(propertyName);
            }
        }

        private void AddError(string propertyName, string errorMessage)
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

        private void ClearErrors(string propertyName)
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
