namespace Zugsichtungen.Domain.Models
{
    public class VehicleViewEntry
    {
        private VehicleViewEntry() { }

        public int? Id { get; private set; }
        public string? Vehicle { get; private set; } = string.Empty;
        public int? SeriesId { get; private set; }

        public static VehicleViewEntry Create(int? id, string? vehicle, int? SeriesId)
        {
            return new VehicleViewEntry() { Id = id, Vehicle = vehicle, SeriesId = SeriesId };
        }
    }
}
