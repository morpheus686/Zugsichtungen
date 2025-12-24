using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Foundation.Enumerations;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.Enumerations;

namespace Zugsichtungen.MAUI.Services
{
    internal class DialogService : Abstractions.Services.IDialogService
    {
        private readonly UraniumUI.Dialogs.IDialogService uraniumDialogService;
        private readonly ViewLocator _viewLocator;
        private readonly Page page;

        public DialogService(UraniumUI.Dialogs.IDialogService uraniumDialogService)
        {
            this.uraniumDialogService = uraniumDialogService;
            _viewLocator = new ViewLocator(typeof(App).Assembly);

            this.page = Application.Current?.Windows.FirstOrDefault()?.Page
                ?? throw new TypeLoadException();
        }

        public async Task<object?> ShowDialogAsync(ILoadable viewModel)
        {
            try
            {
                await viewModel.Initialize();
                var view = _viewLocator.ResolveView(viewModel);

                if (view is Popup popup)
                {
                    popup.BindingContext = viewModel;
                    var result = await page.ShowPopupAsync(popup);

                    if (result == null)
                    {
                        return DialogResult.Abort;
                    }

                    return result;
                }

                return DialogResult.Abort;
            }
            catch (Exception e)
            {
                await this.page.DisplayAlert("Fehler", e.Message, "OK");
                return DialogResult.Abort;
            }
        }

        public async Task ShowIndeterminateDialogAsync(Func<Action<string, IndeterminateState>, object?, Task> progressTask, object? parameter = null)
        {
            var viewModel = new IndeterminateDialogViewModel();
            var progressDialog = await this.uraniumDialogService.DisplayProgressAsync("Speichern", "Bitte warten...");

            try
            {
                await progressTask((message, state) =>
                {
                    viewModel.SetNewMessage(message, state);
                }, parameter);
            }
            finally
            {
                progressDialog.Dispose();
            }
        }

        public async Task<string?> ShowOpenFileDialogAsync(string filter = "Alle Dateien (*.*)|*.*")
        {
            var file = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Datei auswählen"
            });
            return file?.FullPath;
        }

        public string[] ShowOpenFilesDialog(string filter = "Alle Dateien (*.*)|*.*")
        {
            var files = FilePicker.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "Dateien auswählen"
            }).Result;
            return files.Select(f => f.FullPath).ToArray();
        }
    }
}
