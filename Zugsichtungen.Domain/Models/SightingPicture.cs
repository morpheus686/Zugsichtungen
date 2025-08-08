namespace Zugsichtungen.Domain.Models
{
    public class SightingPicture
    {
        public int Id { get; set; }

        public int? SightingId { get; set; }

        public byte[] Image { get; set; } = null!;

        public string Filename { get; set; } = null!;
    }
}
