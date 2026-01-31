namespace Zugsichtungen.Domain.Models.Gallery
{
    public class Picture
    {
        private Picture()
        {
        }

        public int? Id { get; private set; } = null!;
        public DateOnly? Date { get; private set; } = null!;
        public string? VehicleDesignation { get; private set; } = null!;
        public string? Location { get; private set; } = null!;
        public string? Context { get; private set; } = null!;
        public string? Comment { get; private set; } = null!;
        public byte[]? ImageData { get; private set; } = null!;
        public byte[]? ThumbnailData { get; private set; } = null!; 
        public static Picture Create(
            int? id, 
            DateOnly? date,
            string? vehicleDesignation,
            string? location,
            string? context,
            string? comment,
            byte[]? imageData,
            byte[]? thumbnailData)
        {
            return new Picture() 
            { 
                Id = id,
                Date = date,
                VehicleDesignation = vehicleDesignation,
                Location = location,
                Context = context,
                Comment = comment,
                ImageData = imageData,
                ThumbnailData = thumbnailData
            };
        }
    }
}
