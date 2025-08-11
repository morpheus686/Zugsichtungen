using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.ViewModels.Enumerations;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDialogService
    {
        Task<object?> ShowDialogAsync(ILoadable viewModel);
        Task ShowIndeterminateDialogAsync(Func<Action<string, IndeterminateState>, object?, Task> progressTask, object? parameter = null);
        Task<string?> ShowOpenFileDialogAsync(string filter = "Alle Dateien (*.*)|*.*");
        string[] ShowOpenFilesDialog(string filter = "Alle Dateien (*.*)|*.*");
    }
}
