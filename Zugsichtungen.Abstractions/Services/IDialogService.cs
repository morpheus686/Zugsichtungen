using Zugsichtungen.Abstractions.Marker;

namespace Zugsichtungen.Abstractions.Services
{
    public interface IDialogService
    {
        Task<object?> ShowDialog(IDialogViewModel viewModel);
    }
}
