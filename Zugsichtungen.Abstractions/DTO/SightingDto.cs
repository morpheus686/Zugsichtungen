namespace Zugsichtungen.Abstractions.DTO
{
    public record SightingDto
    (
        int Id,
        int VehicleId,
        DateOnly Date,
        string Location,
        int ContextId,
        string? Note
    );
}
