using MaterialDesignThemes.Wpf;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Services
{
    public class DialogService : IDialogService
    {
        private const string DialogIdentifier = "DialogHost";

        public Task<object?> ShowDialog(ILoadable viewModel)
        {
            viewModel.Initialize();
            return DialogHost.Show(viewModel, DialogIdentifier);
        }
    }
}
