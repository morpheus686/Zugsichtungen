using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.ItemViewModels;

namespace Zugsichtungen.Wpf.ViewModels.ItemViewModel
{
    public class GalleryItemWpfViewModel : GalleryItemViewModel
    {
        public GalleryItemWpfViewModel(PictureDto picture, IGalleryService galleryService) : base(picture, galleryService)
        {
        }

        public ImageSource? Thumbnail { get; private set; } = null!;

        public override async Task LoadThumbnailAsync()
        {
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

                    using var ms = new MemoryStream(thumbnailDataDto.Data);

                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.StreamSource = ms;
                    bmp.EndInit();
                    bmp.Freeze(); // 🔥 EXTREM wichtig

                    return bmp;
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
    }
}
