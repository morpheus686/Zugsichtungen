namespace Zugsichtungen.Abstractions.DTO
{
    public class SightingDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public DateOnly Date { get; set; }

        public string Location { get; set; } = string.Empty;

        public int ContextId { get; set; }

        public string? Note { get; set; }
    }
}
