using Zugsichtungen.Abstractions.DTO;

public class SightingWithPictureDto
{
    public SightingDto Sighting { get; set; } = new();
    public SightingPictureDto Picture { get; set; } = new();
}