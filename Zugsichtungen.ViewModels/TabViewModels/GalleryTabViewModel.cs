using System.Collections.ObjectModel;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.ItemViewModels;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public abstract class GalleryTabViewModel : TabViewModelBase
    {
        public GalleryTabViewModel(IGalleryService galleryService)
        {
            this.Title = "Galerie";
            GalleryService = galleryService;

            GalleryList = [];
        }

        protected IGalleryService GalleryService { get; }
        public ObservableCollection<GalleryItemViewModel> GalleryList { get; }

        protected async override Task InitializeInternalAsync()
        {
            GalleryList.Clear();
            var pictures = await GalleryService.GetGalleryPicturesAsync();

            foreach (var picture in pictures)
            {
                GalleryItemViewModel newItem = CreateGalleryItemViewModel(picture);
                await newItem.Initialize();
                GalleryList.Add(newItem);
            }
        }

        protected abstract GalleryItemViewModel CreateGalleryItemViewModel(PictureDto picture);
    }
}
