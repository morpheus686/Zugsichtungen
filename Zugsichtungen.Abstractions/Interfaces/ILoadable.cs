namespace Zugsichtungen.Abstractions.Interfaces
{
    public interface ILoadable
    {
        Task Initialize();
        bool IsInitializing { get; }
    }
}
