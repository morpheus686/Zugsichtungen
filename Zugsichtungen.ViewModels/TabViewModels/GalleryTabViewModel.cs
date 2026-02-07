using System.Collections.ObjectModel;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;
using Zugsichtungen.ViewModels.ItemViewModels;

namespace Zugsichtungen.ViewModels.TabViewModels
{
    public abstract class GalleryTabViewModel : TabViewModelBase
    {
        public GalleryTabViewModel(IGalleryService galleryService, IDialogService dialogService)
        {
            this.Title = "Galerie";
            GalleryService = galleryService;
            DialogService = dialogService;
            GalleryList = [];
        }

        protected IGalleryService GalleryService { get; }
        protected IDialogService DialogService { get; }
        public ObservableCollection<GalleryItemViewModel> GalleryList { get; }

        protected async override Task InitializeInternalAsync()
        {
            var tasks = new List<Task>();

            await this.DialogService.ShowIndeterminateDialogAsync(async (setMessage, obj) =>
            {
                setMessage("Galerie wird geladen.", Enumerations.IndeterminateState.Working);
                GalleryList.Clear();
                var pictures = await GalleryService.GetGalleryPicturesAsync();

                foreach (var picture in pictures)
                {
                    GalleryItemViewModel newItem = CreateGalleryItemViewModel(picture, this.GalleryService, this.DialogService);                    
                    GalleryList.Add(newItem);
                    tasks.Add(newItem.LoadThumbnailAsync());
                }
            });

            await Task.WhenAll(tasks);
        }

        protected abstract GalleryItemViewModel CreateGalleryItemViewModel(
            PictureDto picture, 
            IGalleryService galleryService,
            IDialogService dialogService);
    }
}
