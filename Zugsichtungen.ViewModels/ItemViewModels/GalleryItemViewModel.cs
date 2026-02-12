using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.ItemViewModels
{
    public abstract class GalleryItemViewModel : LoadableViewModel
    {
        private readonly IDialogService dialogService;

        private bool isNoThumbnailAvailable;
        private bool isThumbnailLoading;
        private bool isNoPictureAvailable;
        private bool isPictureLoading;

        public GalleryItemViewModel(
            PictureDto pictureDto,
            IGalleryService galleryService,
            IDialogService dialogService)
        {
            this.PictureDto = pictureDto;
            this.GalleryService = galleryService;
            this.dialogService = dialogService;
            this.IsThumbnailLoading = true;
            this.IsNoThumbnailAvailable = false;
            this.IsPictureLoading = true;
            this.IsNoPictureAvailable = false;

            this.ShowPictureCommand = new AsyncRelayCommand(ExecuteShowPictureCommand);
            
        }

        protected PictureDto PictureDto { get; }
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

        public bool IsNoPictureAvailable
        {
            get => isNoPictureAvailable;
            protected set
            {
                if (isNoPictureAvailable != value)
                {
                    isNoPictureAvailable = value;
                    RaisePropertyChanged(nameof(IsNoPictureAvailable));
                }
            }
        }

        public bool IsPictureLoading
        {
            get => isPictureLoading;
            set
            {
                if (isPictureLoading != value)
                {
                    isPictureLoading = value;
                    RaisePropertyChanged(nameof(IsPictureLoading));
                }
            }
        }

        public ICommand ShowPictureCommand { get; }

        public int? PictureId => PictureDto.PictureId;
        public DateOnly? Date => PictureDto.Date;
        public string? VehicleDesignation => PictureDto.VehicleDesignation;
        public string? Location => PictureDto.Location;

        public abstract Task LoadThumbnailAsync();
        public abstract Task LoadPictureAsync();

        private async Task ExecuteShowPictureCommand()
        {
            await this.dialogService.ShowDialogAsync(this);
        }
    }
}
