using System.Windows;
using Zugsichtungen.ViewModel;

namespace Zugsichtungen.Views
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
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainWindowViewModel mwvm)
            {
                mwvm.Initialize();                
            }
        }
    }
}