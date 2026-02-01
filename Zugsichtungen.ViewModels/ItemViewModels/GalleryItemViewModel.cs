using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.ItemViewModels
{
    public abstract class GalleryItemViewModel : LoadableViewModel
    {
        public GalleryItemViewModel(PictureDto picture)
        {
            this.Picture = picture;
        }

        protected PictureDto Picture { get; }

        public int? Id => Picture.Id;
        public DateOnly? Date => Picture.Date;
        public string? VehicleDesignation => Picture.VehicleDesignation;
        public string? Location => Picture.Location;
        public byte[]? ImageData => Picture.ImageData;
        public byte[]? ThumbnailData => Picture.ThumbnailData;


    }
}
