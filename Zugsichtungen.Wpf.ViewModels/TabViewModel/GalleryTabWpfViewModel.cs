using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.ItemViewModels;
using Zugsichtungen.ViewModels.TabViewModels;
using Zugsichtungen.Wpf.ViewModels.ItemViewModel;

namespace Zugsichtungen.Wpf.ViewModels.TabViewModel
{
    public class GalleryTabWpfViewModel : GalleryTabViewModel
    {
        public GalleryTabWpfViewModel(IGalleryService galleryService) : base(galleryService)
        {
        }

        protected override GalleryItemViewModel CreateGalleryItemViewModel(PictureDto picture)
        {
            return new GalleryItemWpfViewModel(picture);
        }
    }
}
