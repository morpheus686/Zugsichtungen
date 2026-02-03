using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class ShowSightingDetailsDialogViewModel : DialogViewModelBase
    {
        private readonly ISightingService sightingService;
        private readonly SightingViewEntryDto sighting;
        private readonly IDialogService dialogService;
        private byte[]? image;
        private bool pictureIsNotAvailable;

        public byte[]? Image
        {
            get => image;
            private set
            {
                image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        public bool PictureIsNotAvailable
        {
            get { return pictureIsNotAvailable; }
            protected set
            {
                if (value != pictureIsNotAvailable)
                {
                    pictureIsNotAvailable = value;
                    RaisePropertyChanged(nameof(PictureIsNotAvailable));
                }
            }
        }


        public ShowSightingDetailsDialogViewModel(ISightingService sightingService, SightingViewEntryDto sichtung, IDialogService dialogService)
        {
            this.sightingService = sightingService;
            this.sighting = sichtung;
            this.dialogService = dialogService;

            this.Title = "Sichtungsdetails";
            this.pictureIsNotAvailable = false;
        }

        protected override async Task InitializeInternalAsync()
        {
            await dialogService.ShowIndeterminateDialogAsync(async (updateMessage, parameter) =>
            {
                updateMessage("Bild wird geladen", Enumerations.IndeterminateState.Working);
                var picture = await this.sightingService.GetSightingPictureBySightingIdAsync(this.sighting.Id);

                if (picture != null)
                {
                    this.Image = picture.Image;
                }
                else
                {
                    this.Image = null;
                    this.PictureIsNotAvailable = true;
                }
            });
        }
    }
}
