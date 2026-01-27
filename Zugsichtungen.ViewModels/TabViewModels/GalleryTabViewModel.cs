using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public abstract class GalleryTabViewModel : TabViewModelBase
    {
        public GalleryTabViewModel(IGalleryService galleryService)
        {
            this.Title = "Galerie";
            GalleryService = galleryService;
        }

        protected IGalleryService GalleryService { get; }
    }
}
