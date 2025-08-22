using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel
{
    public class ContextItemViewModel : ViewModelBase
    {
        private readonly Context context;

        public int Id => this.context.Id;
        public string Name => this.context.Name;

        public ContextItemViewModel(Context context)
        {
            this.context = context;
        }
    }
}
