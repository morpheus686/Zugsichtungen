using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class AddSichtungDialogViewModel : DialogViewModelBase
    {
        public AddSichtungDialogViewModel(ISightingService sightingService, IDialogService dialogService)
        {
            SelectedDate = DateTime.Now;
            this.sightingService = sightingService;
            this.dialogService = dialogService;
            this.VehicleList = [];
            this.ContextList = [];

            this.AddImageCommand = new AsyncRelayCommand(ExecuteAddImageCommand);
            this.RemoveImageCommand = new RelayCommand(ExecuteRemoveImageCommand);
            this.DropImageCommand = new RelayCommand<string>(ExecuteDropImageCommand, CanExecuteDropImageCommand);
        }

        private DateTime selectedDate;
        private string? imagePath = null;
        private string place = string.Empty;
        private VehicleViewEntryItemViewModel selectedFahrzeug = null!;
        private ContextItemViewModel selectedKontext = null!;
        private readonly ISightingService sightingService;
        private readonly IDialogService dialogService;

        public VehicleViewEntryItemViewModel SelectedFahrzeug
        {
            get => selectedFahrzeug;
            set
            {
                selectedFahrzeug = value;
                RaisePropertyChanged(nameof(SelectedFahrzeug));
            }
        }
        public ContextItemViewModel SelectedKontext
        {
            get => selectedKontext;
            set
            {
                selectedKontext = value;
                RaisePropertyChanged(nameof(SelectedKontext));
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                RaisePropertyChanged(nameof(selectedDate));
            }
        }

        public ObservableCollection<VehicleViewEntryItemViewModel> VehicleList { get; private set; }
        public ObservableCollection<ContextItemViewModel> ContextList { get; private set; }

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
                    RaisePropertyChanged(nameof(ImagePath));
                }
            }
        }
        public ICommand AddImageCommand { get; }
        public ICommand RemoveImageCommand { get; }
        public ICommand DropImageCommand { get; }

        public bool PlaceIsInvalid { get; private set; }

        protected override async Task InitializeInternalAsync()
        {
            await LoadAndSelectFirstAsync(
                this.sightingService.GetVehicleViewEntriesAsync,
                this.VehicleList,
                item => this.SelectedFahrzeug = item,
                item => new VehicleViewEntryItemViewModel(item));

            await LoadAndSelectFirstAsync(
                this.sightingService.GetContextsAsync,
                this.ContextList,
                item => this.SelectedKontext = item,
                item => new ContextItemViewModel(item));

            ValidatePlace();
        }

        private async Task LoadAndSelectFirstAsync<T, TVM>(
            Func<Task<List<T>>> loadFunc,
            ObservableCollection<TVM> targetCollection,
            Action<TVM> setSelectedItem,
            Func<T, TVM> vmFactory)
        {
            var items = await loadFunc();
            targetCollection.Clear();

            foreach (var item in items)
            {
                targetCollection.Add(vmFactory(item));
            }

            if (targetCollection.Any())
            {
                setSelectedItem(targetCollection.First());
            }
        }

        private async Task ExecuteAddImageCommand()
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

        private bool CanExecuteDropImageCommand(string? file)
        {
            if (file == null)
            {
                return false;
            }

            var isNotEmpty = !string.IsNullOrWhiteSpace(file);

            if (isNotEmpty)
            {
                var isValidExtension = file.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                                file.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase);

                if (isValidExtension)
                {
                    return true;
                }
            }

            return false;
        }

        private void ExecuteDropImageCommand(string? obj)
        {
            this.ImagePath = obj;
        }
    }
}
