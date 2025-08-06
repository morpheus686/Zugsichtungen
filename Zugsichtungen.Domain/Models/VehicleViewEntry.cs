namespace Zugsichtungen.Domain.Models
{
    public class VehicleViewEntry
    {
        public int Id { get; set; }
        public string Vehicle { get; set; } = string.Empty;
        public int? SeriesId { get; set; }
    }
}
