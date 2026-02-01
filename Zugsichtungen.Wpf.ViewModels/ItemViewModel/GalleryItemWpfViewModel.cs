using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Domain.Models.Gallery;
using Zugsichtungen.ViewModels.ItemViewModels;

namespace Zugsichtungen.Wpf.ViewModels.ItemViewModel
{
    public class GalleryItemWpfViewModel : GalleryItemViewModel
    {
        public GalleryItemWpfViewModel(PictureDto picture) : base(picture)
        {
        }

        public ImageSource? Thumbnail { get; private set; } = null!;

        protected async override Task InitializeInternalAsync()
        {
            Thumbnail = await Task.Run(() =>
            {
                if (Picture.ThumbnailData != null 
                    && Picture.ThumbnailData.Length == 0)
                {
                    return null;
                }

                using var ms = new MemoryStream(Picture.ThumbnailData);

                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = ms;
                bmp.EndInit();
                bmp.Freeze(); // 🔥 EXTREM wichtig

                return bmp;
            });

            RaisePropertyChanged(nameof(Thumbnail));
        }
    }
}
