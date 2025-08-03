using System.Collections.ObjectModel;
using System.ComponentModel;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class AddSichtungDialogViewModel : DialogViewModelBase, IDataErrorInfo
    {
        public AddSichtungDialogViewModel(ISightingService sichtungService)
        {
            SelectedDate = DateTime.Now;
            this.sichtungService = sichtungService;
            this.VehicleList = [];
            this.ContextList = [];
        }

        private DateTime selectedDate;
        private readonly ISightingService sichtungService;

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
                this.sichtungService.GetAllVehicleViewEntriesAsync,
                this.VehicleList,
                item => this.SelectedFahrzeug = item,
                nameof(SelectedFahrzeug));

            await LoadAndSelectFirstAsync(
                this.sichtungService.GetAllContextesAsync,
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
