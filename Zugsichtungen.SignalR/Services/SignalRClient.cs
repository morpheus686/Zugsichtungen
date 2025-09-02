using Microsoft.AspNetCore.SignalR.Client;

namespace Zugsichtungen.SignalR.Services
{
    public class SignalRClient : ISignalRClient
    {
        private readonly HubConnection connection;

        public SignalRClient(HubConnection connection)
        {
            this.connection = connection;
        }

        public async ValueTask DisposeAsync()
        {
            await connection.DisposeAsync();
        }

        public void On<T>(string methodName, Action<T> handler)
        {
            connection.On(methodName, handler);
        }

        public async Task StopAsync()
        {
            await connection.StopAsync();
        }
    }
}