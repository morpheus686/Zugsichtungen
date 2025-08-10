using Zugsichtungen.ViewModels.TabViewModels;


namespace Zugsichtungen.MAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(SightingOverviewTabViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object? sender, EventArgs e)
        {
            if (this.BindingContext is SightingOverviewTabViewModel mwvm)
            {
                await mwvm.Initialize();
            }
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
