using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDialogService
    {
        Task<object?> ShowDialogAsync(ILoadable viewModel);
        Task ShowIndeterminateDialogAsync(Func<Action<string>, object?, Task> progressTask, object? parameter = null);
    }
}
