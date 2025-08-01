using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class EditContextesDialogViewModel : DialogViewModelBase
    {
        private readonly ISichtungService sichtungService;

        public override bool HasErrors => false;

        public EditContextesDialogViewModel(ISichtungService sichtungService)
        {
            this.sichtungService = sichtungService;
        }

        protected override Task InitializeInternalAsync()
        {
            return base.InitializeInternalAsync();
        }
    }
}
