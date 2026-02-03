namespace Zugsichtungen.Abstractions.DTO
{
    public record PictureDto(int? SightingId,
            int? PictureId,
            DateOnly? Date,
            string? VehicleDesignation,
            string? Location,
            string? Context,
            string? Comment)
    {
    }
}
