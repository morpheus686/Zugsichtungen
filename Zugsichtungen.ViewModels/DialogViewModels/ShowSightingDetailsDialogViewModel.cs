using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.DialogViewModels
{
    public class ShowSightingDetailsDialogViewModel : DialogViewModelBase
    {
        private readonly ISightingService sightingService;
        private readonly SightingViewEntry sighting;
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

        public override bool HasErrors => false;

        public ShowSightingDetailsDialogViewModel(ISightingService sightingService, SightingViewEntry sichtung, IDialogService dialogService)
        {
            this.sightingService = sightingService;
            this.sighting = sichtung;
            this.dialogService = dialogService;

            this.Title = "Sichtungsdetails";
        }

        protected override async Task InitializeInternalAsync()
        {
            await dialogService.ShowIndeterminateDialogAsync(async (updateMessage, parameter) =>
            {
                updateMessage("Bild wird geladen", Enumerations.IndeterminateState.Working);
                var picture = await this.sightingService.GetSightingPictureByIdAsync(this.sighting.Id);

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
