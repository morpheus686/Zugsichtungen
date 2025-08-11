using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class EditContextesDialogViewModel : DialogViewModelBase
    {
        private readonly ISightingService sichtungService;

        public EditContextesDialogViewModel(ISightingService sichtungService)
        {
            this.sichtungService = sichtungService;
        }

        protected override Task InitializeInternalAsync()
        {
            return base.InitializeInternalAsync();
        }
    }
}
