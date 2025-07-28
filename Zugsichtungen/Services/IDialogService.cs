using Zugsichtungen.ViewModel;

namespace Zugsichtungen.Services
{
    public interface IDialogService
    {
        Task<object?> ShowDialog(ViewModelBase viewModel);
    }
}
