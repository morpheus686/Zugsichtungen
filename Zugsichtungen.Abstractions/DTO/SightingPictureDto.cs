namespace Zugsichtungen.Abstractions.DTO
{
    public class SightingPictureDto
    {
        public int Id { get; set; }

        public int? SightingId { get; set; }

        public byte[] Image { get; set; } = null!;

        public byte[]? Thumbnail { get; set; } = null!;

        public string Filename { get; set; } = null!;
    }
}
