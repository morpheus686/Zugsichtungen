namespace Zugsichtungen.Abstractions.DTO
{
    public record PictureDto(int? Id,
            DateOnly? Date,
            string? VehicleDesignation,
            string? Location,
            string? Context,
            string? Comment,
            byte[]? ImageData)
    {
    }
}
