namespace Zugsichtungen.Domain.Models
{
    public class SightingViewEntry
    {
        public int? Id { get; set; }

        public DateOnly? Date { get; set; }

        public string? VehicleNumber { get; set; }

        public string? Location { get; set; }

        public string? Context { get; set; }

        public string? Note { get; set; }
        public byte[] Image { get; set; }
    }
}
