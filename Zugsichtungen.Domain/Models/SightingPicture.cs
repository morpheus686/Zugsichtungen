namespace Zugsichtungen.Domain.Models
{
    public class SightingPicture
    {
        private SightingPicture() { }

        public int Id { get; private set; }

        public int SightingId { get; private set; }

        public byte[] Image { get; private set; } = null!;
        public byte[]? Thumbnail { get; private set; }

        public string Filename { get; private set; } = null!;

        public static SightingPicture Create(int id, int sightingId,  byte[] image, byte[]? thumbnail, string filename)
        {
            return new SightingPicture
            {
                Id = id,
                SightingId = sightingId,
                Image = image,
                Thumbnail = thumbnail,
                Filename = filename
            };
        }
    }
}
