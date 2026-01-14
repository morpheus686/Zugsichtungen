namespace Zugsichtungen.Abstractions.Interfaces
{
    public interface ICheckable
    {
        int Id { get; }
        bool IsChecked { get; set; }
        string? Text { get; }
    }
}
