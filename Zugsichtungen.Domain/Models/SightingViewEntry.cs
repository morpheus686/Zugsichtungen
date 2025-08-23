namespace Zugsichtungen.Domain.Models
{
    public class SightingViewEntry
    {
        private SightingViewEntry()
        {

        }

        public int? Id { get; private set; }

        public DateOnly? Date { get; private set; }

        public string? VehicleNumber { get; private set; }

        public string? Location { get; private set; }

        public string? Context { get; private set; }

        public string? Note { get; private set; }
        public byte[]? Thumbnail { get; private set; }

        public static SightingViewEntry Create(int? id,
            DateOnly? date,
            string? vehicleNumber,
            string? location,
            string? context,
            string? note,
            byte[]? thumbnail)
        {
            return new SightingViewEntry
            {
                Id = id,
                Date = date,
                VehicleNumber = vehicleNumber,
                Location = location,
                Context = context,
                Note = note,
                Thumbnail = thumbnail
            };
        }
    }
}
