using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.ViewModels
{
    public class MainWindowViewModel : LoadableViewModel
    {
        private TabViewModelBase selectedTab;
        private bool isDrawerOpen;
        private readonly IDialogService dialogService;

        public ICommand SelectTabCommand { get; }
        public ICommand? ToggleDrawerCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public SichtungItemViewModel? SelectedItem { get; set; }
        public SightingOverviewTabViewModelBase SightingOverviewTabViewModel { get; }
        public GalleryTabViewModel GalleryTabViewModel { get; }

        public string CurrentTabTitle => SelectedTab.Title;

        public bool IsDrawerOpen
        {
            get => isDrawerOpen;
            set
            {
                if (this.isDrawerOpen != value)
                {
                    isDrawerOpen = value;
                    RaisePropertyChanged(nameof(IsDrawerOpen));
                }
            }
        }

        public TabViewModelBase SelectedTab
        {
            get => selectedTab;
            private set
            {
                selectedTab = value;
                RaisePropertyChanged(nameof(SelectedTab));
            }
        }

        public MainWindowViewModel(SightingOverviewTabViewModelBase sightingOverviewTabViewModel, 
            GalleryTabViewModel galleryTabViewModel,
            IDialogService dialogService)
        {
            this.SelectTabCommand = new RelayCommand<TabViewModelBase>(ExecuteSelectTabCommand);
            this.ToggleDrawerCommand = new RelayCommand(() => IsDrawerOpen = !IsDrawerOpen);
            this.OpenSettingsCommand = new AsyncRelayCommand(ExecuteOpenSettingsAsync);

            this.GalleryTabViewModel = galleryTabViewModel;
            this.dialogService = dialogService;
            this.SightingOverviewTabViewModel = sightingOverviewTabViewModel;
            this.selectedTab = SightingOverviewTabViewModel;
        }

        private async Task ExecuteOpenSettingsAsync()
        {
            await this.dialogService.ShowDialogAsync(new SettingsDialogViewModel());
        }

        private void ExecuteSelectTabCommand(TabViewModelBase? tabViewModel)
        {
            if (tabViewModel != null && this.SelectedTab != tabViewModel)
            {
                this.SelectedTab = tabViewModel;
                RaisePropertyChanged(nameof(CurrentTabTitle));
            }

            this.IsDrawerOpen = false;
        }

        protected override Task InitializeInternalAsync()
        {
            return this.selectedTab.Initialize();
        }
    }
}
