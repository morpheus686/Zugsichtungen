namespace Zugsichtungen.Domain.Models.Gallery
{
    public class Picture
    {
        private Picture()
        {
        }

        public int? SightingId { get; private set; } = null!;
        public int? PictureId { get; private set; } = null!;
        public DateOnly? Date { get; private set; } = null!;
        public string? VehicleDesignation { get; private set; } = null!;
        public string? Location { get; private set; } = null!;
        public string? Context { get; private set; } = null!;
        public string? Comment { get; private set; } = null!;

        public static Picture Create(
            int? sightingId,
            int? pictureId,
            DateOnly? date,
            string? vehicleDesignation,
            string? location,
            string? context,
            string? comment)
        {
            return new Picture() 
            { 
                SightingId = sightingId,
                PictureId = pictureId,
                Date = date,
                VehicleDesignation = vehicleDesignation,
                Location = location,
                Context = context,
                Comment = comment
            };
        }
    }
}
