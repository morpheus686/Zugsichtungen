using MaterialDesignThemes.Wpf;
using Zugsichtungen.ViewModel;

namespace Zugsichtungen
{
    public class DialogHostManager
    {
        private const string DialogIdentifier = "DialogHost";

        public Task<object?> ShowDialog(ViewModelBase viewModel)
        {
            return DialogHost.Show(viewModel, DialogIdentifier);
        }

        //public Task ShowMessage(string message)
        //{
        //    return Task.CompletedTask;
        //}

        private DialogHost LoadDialogHost()
        {
            if (App.Current.MainWindow is not MainWindow mainWindow)
            {
                throw new InvalidOperationException("MainWindow wurde nicht gefunden!");
            }

            return mainWindow.DialogHost;
        }
    }
}
