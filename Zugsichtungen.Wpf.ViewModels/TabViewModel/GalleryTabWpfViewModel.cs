using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.Wpf.ViewModels.TabViewModel
{
    public class GalleryTabWpfViewModel : GalleryTabViewModel
    {
        public GalleryTabWpfViewModel(IGalleryService galleryService) : base(galleryService)
        {
        }
    }
}
