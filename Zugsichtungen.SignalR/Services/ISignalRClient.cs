
namespace Zugsichtungen.SignalR.Services
{    public interface ISignalRClient
    {
        void On<T>(string methodName, Action<T> handler);
    }
}

