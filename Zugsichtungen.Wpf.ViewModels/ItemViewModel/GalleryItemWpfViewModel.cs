using System.Windows.Media;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.ViewModels.ItemViewModels;

namespace Zugsichtungen.Wpf.ViewModels.ItemViewModel
{
    public class GalleryItemWpfViewModel : GalleryItemViewModel
    {
        public GalleryItemWpfViewModel(PictureDto picture) : base(picture)
        {
        }

        //public ImageSource? Thumbnail
        //=> ThumbnailData == null
        //   ? null
        //   : ImageSourceHelper.FromBytes(ThumbnailData);
    }
}
