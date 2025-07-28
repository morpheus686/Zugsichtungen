using MaterialDesignThemes.Wpf;
using Zugsichtungen.ViewModel;

namespace Zugsichtungen.Services
{
    public class DialogService : IDialogService
    {
        private const string DialogIdentifier = "DialogHost";

        public Task<object?> ShowDialog(ViewModelBase viewModel)
        {
            return DialogHost.Show(viewModel, DialogIdentifier);
        }
    }
}
