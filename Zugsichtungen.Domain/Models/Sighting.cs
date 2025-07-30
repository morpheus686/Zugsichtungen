namespace Zugsichtungen.Domain.Models
{
    public class Sighting
    {
        public int? Id { get; set; }

        public int? VehicleId { get; set; }

        public DateOnly? Date { get; set; }

        public string? Location { get; set; }

        public int? ContextId { get; set; }

        public string? Note { get; set; }
    }
}
