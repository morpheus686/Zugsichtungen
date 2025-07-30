namespace Zugsichtungen.Abstractions.DTO
{
    public class SightingViewEntryDto
    {
        public int? Id { get; set; }

        public DateOnly? Date { get; set; }

        public string? VehicleNumber { get; set; }

        public string? Location { get; set; }

        public string? Context { get; set; }

        public string? Note { get; set; }
    }
}
