using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.Enumerations;

namespace Zugsichtungen.MAUI.Services
{
    internal class DialogService : IDialogService
    {
        public Task<object?> ShowDialog(ILoadable viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<object?> ShowDialogAsync(ILoadable viewModel)
        {
            throw new NotImplementedException();
        }

        public Task ShowIndeterminateDialogAsync(Func<Action<string, IndeterminateState>, object?, Task> progressTask, object? parameter = null)
        {
            throw new NotImplementedException();
        }

        public string? ShowOpenFileDialog(string filter = "Alle Dateien (*.*)|*.*")
        {
            throw new NotImplementedException();
        }

        public string[] ShowOpenFilesDialog(string filter = "Alle Dateien (*.*)|*.*")
        {
            throw new NotImplementedException();
        }
    }
}
