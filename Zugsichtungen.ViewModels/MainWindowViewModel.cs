using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.ViewModels
{
    public class MainWindowViewModel : LoadableViewModel
    {
        private TabViewModelBase selectedTab;
        private bool isDrawerOpen;

        public SichtungItemViewModel? SelectedItem { get; set; }

        public SightingOverviewTabViewModel SightingOverviewTabViewModel { get; }
        public GalleryTabViewModel GalleryTabViewModel { get; }
        public ICommand SelectTabCommand { get; }
        public ICommand? ToggleDrawerCommand { get; set; }
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

        public MainWindowViewModel(SightingOverviewTabViewModel sightingOverviewTabViewModel, GalleryTabViewModel galleryTabViewModel)
        {
            this.SelectTabCommand = new RelayCommand<TabViewModelBase>(ExecuteSelectTabCommand);
            this.ToggleDrawerCommand = new RelayCommand(() => IsDrawerOpen = !IsDrawerOpen);

            this.GalleryTabViewModel = galleryTabViewModel;
            this.SightingOverviewTabViewModel = sightingOverviewTabViewModel;
            this.selectedTab = SightingOverviewTabViewModel;
        }

        private void ExecuteSelectTabCommand(TabViewModelBase? tabViewModel)
        {
            if (tabViewModel != null)
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
