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
        private bool isDrawerOpen;
        private readonly IDialogService dialogService;

        public ICommand SelectTabCommand { get; }
        public ICommand? ToggleDrawerCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public SichtungItemViewModel? SelectedItem { get; set; }
        public SightingOverviewTabViewModel SightingOverviewTabViewModel { get; }
        public GalleryTabViewModel GalleryTabViewModel { get; }
        public ISnackbarService SnackbarService { get; }

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

        public TabViewModelBase SelectedTab { get; private set; }

        public MainWindowViewModel(SightingOverviewTabViewModel sightingOverviewTabViewModel, 
            GalleryTabViewModel galleryTabViewModel,
            IDialogService dialogService,
            ISnackbarService snackbarService)
        {
            this.SelectTabCommand = new AsyncRelayCommand<TabViewModelBase>(ExecuteSelectTabCommand);
            this.ToggleDrawerCommand = new RelayCommand(() => IsDrawerOpen = !IsDrawerOpen);
            this.OpenSettingsCommand = new AsyncRelayCommand(ExecuteOpenSettingsAsync);

            this.GalleryTabViewModel = galleryTabViewModel;
            this.dialogService = dialogService;
            SnackbarService = snackbarService;
            this.SightingOverviewTabViewModel = sightingOverviewTabViewModel;
            SelectedTab = SightingOverviewTabViewModel;
        }

        private async Task ExecuteOpenSettingsAsync()
        {
            await this.dialogService.ShowDialogAsync(new SettingsDialogViewModel());
        }

        private async Task ExecuteSelectTabCommand(TabViewModelBase? tabViewModel)
        {
            if (tabViewModel != null && this.SelectedTab != tabViewModel)
            {
                this.SelectedTab = tabViewModel;
                await this.SelectedTab.Initialize();
                RaisePropertyChanged(nameof(SelectedTab));
                RaisePropertyChanged(nameof(CurrentTabTitle));
            }

            this.IsDrawerOpen = false;
        }

        protected override Task InitializeInternalAsync()
        {
            return SelectedTab.Initialize();
        }
    }
}
