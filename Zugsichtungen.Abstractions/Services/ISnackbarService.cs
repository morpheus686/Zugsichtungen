namespace Zugsichtungen.Abstractions.Services
{
    public interface ISnackbarService
    {
        void Show(string message);
        object SnackbarMessageQueue { get; }
    }
}
