using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Zugsichtungen.ViewModels;

namespace Zugsichtungen.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            this.Loaded += MainWindow_Loaded;
            this.ContentRendered += MainWindow_ContentRendered;
        }

        private void MainWindow_ContentRendered(object? sender, EventArgs e)
        {
            Debug.WriteLine("MainWindow rendered");
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainWindowViewModel mwvm)
            {
                await mwvm.Initialize();                
            }
        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }

                this.DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}