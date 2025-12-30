using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels.ItemViewModel
{
    public class SeriesItemViewModel : ViewModelBase
    {
        private readonly SeriesDto seriesDto;

        public int Id => seriesDto.Id;
        public string? Number => seriesDto.Number;

        public SeriesItemViewModel(SeriesDto seriesDto)
        {
            this.seriesDto = seriesDto;
        }
    }
}
