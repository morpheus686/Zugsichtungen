using System.Collections.ObjectModel;
using System.ComponentModel;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Marker;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class AddSichtungDialogViewModel : DialogViewModelBase, IDataErrorInfo, IDialogViewModel
    {
        public AddSichtungDialogViewModel(IDataService dataService)
        {
            SelectedDate = DateTime.Now;
            this.dataService = dataService;
            this.VehicleList = [];
            this.ContextList = [];
        }

        private DateTime selectedDate;
        private readonly IDataService dataService;

        public VehicleViewEntryDto SelectedFahrzeug { get; set; } = null!;
        public ContextDto SelectedKontext { get; set; } = null!;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set 
            { 
                selectedDate = value;
                RaisePropertyChanged(nameof(selectedDate));
            }
        }

        public ObservableCollection<VehicleViewEntryDto> VehicleList { get; private set; }
        public ObservableCollection<ContextDto> ContextList { get; private set; }

        public string Note { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;

        public string this[string columnName]
        {
            get 
            {
                if (columnName == nameof(Place))
                {
                    if (string.IsNullOrWhiteSpace(this.Place))
                    {                        
                        this.PlaceIsInvalid = true;
                        RaisePropertyChanged(nameof(HasErrors));
                        return "Der Ort darf nicht leer sein!";
                    }
                    else
                    {
                        this.PlaceIsInvalid = false;
                        RaisePropertyChanged(nameof(HasErrors));
                    }
                }

                return string.Empty; 
            }
        }

        public string Error => String.Empty;

        public bool PlaceIsInvalid { get; private set; }
        public override bool HasErrors => this.PlaceIsInvalid;

        protected override async Task InitializeInternalAsync()
        {
            await LoadAndSelectFirstAsync(
                this.dataService.GetAllFahrzeugeAsync,
                this.VehicleList,
                item => this.SelectedFahrzeug = item,
                nameof(SelectedFahrzeug));

            await LoadAndSelectFirstAsync(
                this.dataService.GetKontextesAsync,
                this.ContextList,
                item => this.SelectedKontext = item,
                nameof(SelectedKontext));
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
    }
}
