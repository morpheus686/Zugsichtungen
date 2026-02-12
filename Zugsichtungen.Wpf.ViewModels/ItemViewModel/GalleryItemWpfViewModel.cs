using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.ItemViewModels;

namespace Zugsichtungen.Wpf.ViewModels.ItemViewModel
{
    public class GalleryItemWpfViewModel : GalleryItemViewModel
    {
        private const double MinimumScale = 1.0;
        private const double MaximumScale = 5.0;
        private const double ScaleStep = 0.1;
        private double scale;

        public GalleryItemWpfViewModel(
            PictureDto picture,
            IGalleryService galleryService,
            IDialogService dialogService) : base(picture, galleryService, dialogService)
        {
            this.ZoomCommand = new RelayCommand<int>(ExecuteZoom);
            this.ResetZoomCommand = new RelayCommand(ExecuteResetZoom);
        }

        public double Scale
        {
            get => scale;
            private set
            {
                if (value >= MinimumScale && value <= MaximumScale)
                {
                    scale = value;
                    RaisePropertyChanged(nameof(Scale));
                }
            }
        }
        public ImageSource? Thumbnail { get; private set; } = null!;
        public ImageSource? Picture { get; private set; } = null!;

        public ICommand ZoomCommand { get; }
        public ICommand ResetZoomCommand { get; }

        public override async Task LoadPictureAsync()
        {
            if (this.Picture != null)
            {
                return;
            }

            var pictureDataDto = await this.GalleryService.GetGalleryPictureDataDtoAsync(this.PictureId.Value);
            await Task.Delay(1500); // Simuliere Ladezeit

            if (pictureDataDto != null)
            {
                Picture = await Task.Run(() =>
                {
                    if (pictureDataDto.Data != null
                        && pictureDataDto.Data.Length == 0)
                    {
                        return null;
                    }

                    return CreateBitmapImage(pictureDataDto.Data);
                });
            }

            this.IsPictureLoading = false;
            RaisePropertyChanged(nameof(IsPictureLoading));

            if (this.Picture == null)
            {
                this.IsNoPictureAvailable = true;
            }
            else
            {
                RaisePropertyChanged(nameof(Picture));
            }
        }

        public override async Task LoadThumbnailAsync()
        {
            if (this.Thumbnail != null)
            {
                return;
            }

            var thumbnailDataDto = await this.GalleryService.GetThumbnailDataAsync(this.PictureId.Value);

            await Task.Delay(2000); // Simuliere Ladezeit

            if (thumbnailDataDto != null)
            {
                Thumbnail = await Task.Run(() =>
                {
                    if (thumbnailDataDto.Data != null
                        && thumbnailDataDto.Data.Length == 0)
                    {
                        return null;
                    }

                    return CreateBitmapImage(thumbnailDataDto.Data);
                });
            }

            this.IsThumbnailLoading = false;
            RaisePropertyChanged(nameof(IsThumbnailLoading));

            if (this.Thumbnail == null)
            {
                this.IsNoThumbnailAvailable = true;
            }
            else
            {
                RaisePropertyChanged(nameof(Thumbnail));
            }
        }

        private static BitmapImage CreateBitmapImage(byte[] data)
        {
            using var ms = new MemoryStream(data);

            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.StreamSource = ms;
            bmp.EndInit();
            bmp.Freeze(); // 🔥 EXTREM wichtig

            return bmp;
        }

        protected override async Task InitializeInternalAsync()
        {
            this.Scale = MinimumScale;
            await this.LoadPictureAsync();
        }

        private void ExecuteZoom(int delta)
        {
            if (delta > 0)
            {
                this.Scale += ScaleStep;
            }
            else if (delta < 0)
            {
                this.Scale -= ScaleStep;
            }
        }

        private void ExecuteResetZoom()
        {
            this.Scale = MinimumScale;
        }
    }
}
