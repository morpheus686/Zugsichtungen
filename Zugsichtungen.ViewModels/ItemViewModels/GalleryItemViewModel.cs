using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels.ItemViewModels
{
    public abstract class GalleryItemViewModel : LoadableViewModel
    {
        private readonly PictureDto picture;

        public GalleryItemViewModel(PictureDto picture)
        {
            this.picture = picture;
        }

        public int? Id => picture.Id;
        public DateOnly? Date => picture.Date;
        public string? VehicleDesignation => picture.VehicleDesignation;
        public string? Location => picture.Location;
        public byte[]? ImageData => picture.ImageData;
        public byte[]? ThumbnailData { get; private set; }
        protected override async Task InitializeInternalAsync()
        {
            await Task.CompletedTask;
        }

        //private async Task<byte[]?> CreateThumbnailAsync(byte[]? imageData)
        //{
        //    if (imageData == null)
        //    {
        //        return null;
        //    }

        //    return await Task.Run(() =>
        //    {
        //        // Decode → Resize → Encode
        //        return resizedBytes;
        //    });
        //}
    }
}
