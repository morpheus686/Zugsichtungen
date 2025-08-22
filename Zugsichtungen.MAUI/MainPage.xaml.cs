using UraniumUI.Pages;
using Zugsichtungen.ViewModels;

namespace Zugsichtungen.MAUI
{
    public partial class MainPage : UraniumContentPage
    {
        private MainWindowViewModel ViewModel { get; }

        public MainPage(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = viewModel;
            this.ViewModel = viewModel;

            viewModel.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(viewModel.IsDrawerOpen))
                    await ToggleDrawerAnimation(viewModel.IsDrawerOpen);
            };


            this.Loaded += MainWindow_Loaded;
        }

        private async Task ToggleDrawerAnimation(bool open)
        {
            if (open)
            {
                Overlay.IsVisible = true;
                Overlay.InputTransparent = false;

                await Task.WhenAll(
                    Drawer.TranslateTo(0, 0, 250, Easing.SinOut),
                    Overlay.FadeTo(0.5, 250, Easing.SinOut)
                );
            }
            else
            {
                await Task.WhenAll(
                    Drawer.TranslateTo(-Drawer.Width, 0, 250, Easing.SinIn),
                    Overlay.FadeTo(0, 250, Easing.SinIn)
                );

                Overlay.IsVisible = false;
                Overlay.InputTransparent = true;
            }
        }

        private void OnOverlayTapped(object sender, TappedEventArgs e)
        {
            ViewModel.IsDrawerOpen = false;
        }

        private async void MainWindow_Loaded(object? sender, EventArgs e)
        {
            if (this.BindingContext is MainWindowViewModel mwvm)
            {
                await mwvm.Initialize();
            }
        }
    }
}
