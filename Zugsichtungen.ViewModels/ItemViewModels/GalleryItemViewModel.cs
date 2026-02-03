using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.ItemViewModels
{
    public abstract class GalleryItemViewModel : LoadableViewModel
    {
        private bool isNoThumbnailAvailable;
        private bool isThumbnailLoading;

        public GalleryItemViewModel(PictureDto picture, IGalleryService galleryService)
        {
            this.Picture = picture;
            GalleryService = galleryService;
            this.IsThumbnailLoading = true;
            this.IsNoThumbnailAvailable = false;
        }

        protected PictureDto Picture { get; }
        public IGalleryService GalleryService { get; }
        public bool IsThumbnailLoading
        {
            get => isThumbnailLoading;
            protected set
            {
                if (isThumbnailLoading != value)
                {
                    isThumbnailLoading = value;
                    RaisePropertyChanged(nameof(IsThumbnailLoading));
                }
            }
        }

        public bool IsNoThumbnailAvailable
        {
            get => isNoThumbnailAvailable;
            protected set
            {
                if (isNoThumbnailAvailable != value)
                {
                    isNoThumbnailAvailable = value;
                    RaisePropertyChanged(nameof(IsNoThumbnailAvailable));
                }
            }
        }

        public int? PictureId => Picture.PictureId;
        public DateOnly? Date => Picture.Date;
        public string? VehicleDesignation => Picture.VehicleDesignation;
        public string? Location => Picture.Location;

        public abstract Task LoadThumbnailAsync();
    }
}
