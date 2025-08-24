namespace Zugsichtungen.Domain.Models
{
    public class Sighting
    {
        private Sighting()
        {
            this.pictures = [];
        }

        private readonly List<SightingPicture> pictures;
        public IReadOnlyCollection<SightingPicture> Pictures => pictures.AsReadOnly();
        public SightingPicture? SightingPicture => pictures.FirstOrDefault();

        public int Id { get; private set; }

        public int VehicleId { get; private set; }

        public DateOnly Date { get; private set; }

        public string Location { get; private set; } = null!;

        public int ContextId { get; private set; }

        public string? Note { get; private set; }

        public static Sighting Create(int id, int vehicleId, DateOnly date, string location, int contextId, string? note)
        {
            return new Sighting()
            {
                Id = id,
                VehicleId = vehicleId,
                Date = date,
                Location = location,
                ContextId = contextId,
                Note = note
            };
        }

        public void AddPicture(SightingPicture sightingPicture)
        {
            if (pictures.Any())
            {
                throw new InvalidOperationException("Es ist nur ein Bild pro Sichtung erlaubt!");
            }

            pictures.Add(sightingPicture);
        }
    }
}
