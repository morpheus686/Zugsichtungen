using Zugsichtungen.Abstractions.DTO;

public record SightingWithPictureDto
(
    SightingDto Sighting,
    SightingPictureDto? Picture
);