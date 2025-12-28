namespace Zugsichtungen.Abstractions.DTO
{
    public record SightingPictureDto
    (
        int Id,
        int? SightingId,
        byte[] Image,
        byte[]? Thumbnail,
        string Filename
    );
}
