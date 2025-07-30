using MaterialDesignThemes.Wpf;
using Zugsichtungen.Abstractions.Marker;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModel;

namespace Zugsichtungen.Services
{
    public class DialogService : IDialogService
    {
        private const string DialogIdentifier = "DialogHost";

        public Task<object?> ShowDialog(IDialogViewModel viewModel)
        {
            return DialogHost.Show(viewModel, DialogIdentifier);
        }
    }
}
