using MaterialDesignThemes.Wpf;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.DialogViewModels;

namespace Zugsichtungen.Services
{
    public class DialogService : IDialogService
    {
        private const string DialogIdentifier = "DialogHost";

        public Task<object?> ShowDialogAsync(ILoadable viewModel)
        {
            viewModel.Initialize();
            return DialogHost.Show(viewModel, DialogIdentifier);
        }

        public async Task ShowIndeterminateDialogAsync(Func<Action<string>, object?, Task> progressTask, object? parameter = null)
        {
            var viewModel = new IndeterminateDialogViewModel();
            var showDialogTask = DialogHost.Show(viewModel, DialogIdentifier);

            void updateMessage(string newMessage)
            {
                viewModel.Message = newMessage;
            }

            try
            {
                await progressTask(updateMessage, parameter);
            }
            finally
            {
                DialogHost.Close(DialogIdentifier, viewModel);
                await showDialogTask;
            }
        }
    }
}
