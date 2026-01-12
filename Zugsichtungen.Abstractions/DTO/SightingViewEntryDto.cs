namespace Zugsichtungen.Abstractions.DTO
{
    public record SightingViewEntryDto
    (
        int Id,
        DateOnly? Date,
        string? VehicleNumber,
        string? Location,
        string? Context,
        string? Note,
        byte[]? Image,
        byte[]? Thumbnail,
        int? VehicleId
    );
}
