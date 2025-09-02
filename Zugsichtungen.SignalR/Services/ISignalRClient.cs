
namespace Zugsichtungen.SignalR.Services
{    public interface ISignalRClient
    {
        void On<T>(string methodName, Action<T> handler);
        Task SendAsync(string methodName, object arg);
    }
}

