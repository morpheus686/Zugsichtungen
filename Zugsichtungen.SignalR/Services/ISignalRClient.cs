
namespace Zugsichtungen.SignalR.Services
{    public interface ISignalRClient : IAsyncDisposable
    {
        void On<T>(string methodName, Action<T> handler);
        Task StopAsync();
    }
}

