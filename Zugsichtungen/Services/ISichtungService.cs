namespace Zugsichtungen.Services
{
    public interface ISichtungService
    {
        Task AddSichtung(DateOnly date, int? vehicleId, int? kontextId, string place, string? note);
    }
}
