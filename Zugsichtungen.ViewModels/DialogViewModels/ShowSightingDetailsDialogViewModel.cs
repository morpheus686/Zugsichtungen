using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class ShowSightingDetailsDialogViewModel : DialogViewModelBase
    {
        private readonly IDataService dataService;
        private readonly SightingViewEntryDto sighting;
        private readonly IDialogService dialogService;
        private byte[]? image;

        public byte[]? Image
        {
            get => image;
            private set 
            { 
                image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        public ShowSightingDetailsDialogViewModel(IDataService dataService, SightingViewEntryDto sichtung, IDialogService dialogService)
        {
            this.dataService = dataService;
            this.sighting = sichtung;
            this.dialogService = dialogService;

            this.Title = "Sichtungsdetails";
        }

        protected override async Task InitializeInternalAsync()
        {
            await dialogService.ShowIndeterminateDialogAsync(async (updateMessage, parameter) =>
            {
                updateMessage("Bild wird geladen", Enumerations.IndeterminateState.Working);
                var picture = await this.dataService.GetSightingPictureBySightingIdAsync(this.sighting.Id);

                if (picture != null)
                {
                    this.Image = picture.Image;
                }
                else
                {
                    this.Image = null;
                }
            });
        }
    }
}
