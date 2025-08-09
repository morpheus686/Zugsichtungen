using MaterialDesignThemes.Wpf;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.Enumerations;

namespace Zugsichtungen.Services
{
    public class DialogService : IDialogService
    {
        private const string DialogIdentifier = "DialogHost";
        private const string ProgressDialogIdentifier = "ProgressDialogHost";

        public Task<object?> ShowDialogAsync(ILoadable viewModel)
        {
            viewModel.Initialize();
            return DialogHost.Show(viewModel, DialogIdentifier);
        }

        public async Task ShowIndeterminateDialogAsync(Func<Action<string, IndeterminateState>, object?, Task> progressTask, object? parameter = null)
        {
            var viewModel = new IndeterminateDialogViewModel();
            var showDialogTask = DialogHost.Show(viewModel, ProgressDialogIdentifier);

            void updateMessage(string newMessage, IndeterminateState indeterminateState)
            {
                viewModel.SetNewMessage(newMessage, indeterminateState);
            }

            try
            {
                await progressTask(updateMessage, parameter);
            }
            finally
            {
                DialogHost.Close(ProgressDialogIdentifier, viewModel);
                await showDialogTask;
            }
        }

        public string? ShowOpenFileDialog(string filter = "Alle Dateien (*.*)|*.*")
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = filter,
                Multiselect = false
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        public string[] ShowOpenFilesDialog(string filter = "Alle Dateien (*.*)|*.*")
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = filter,
                Multiselect = true
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileNames : [];
        }
    }
}
