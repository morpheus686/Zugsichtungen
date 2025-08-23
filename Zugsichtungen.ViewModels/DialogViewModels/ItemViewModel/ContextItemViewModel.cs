using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel
{
    public class ContextItemViewModel : ViewModelBase
    {
        private readonly ContextDto context;

        public int Id => this.context.Id;
        public string Name => this.context.Name;

        public ContextItemViewModel(ContextDto context)
        {
            this.context = context;
        }
    }
}
