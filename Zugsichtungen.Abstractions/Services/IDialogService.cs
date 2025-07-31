using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDialogService
    {
        Task<object?> ShowDialog(ILoadable viewModel);
    }
}
