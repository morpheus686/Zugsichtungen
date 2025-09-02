using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ApplicationBase;
using Zugsichtungen.SignalR.Services;
using Zugsichtungen.SignalR.ViewModels.TabViewModels;
using Zugsichtungen.ViewModels.TabViewModels;
using Zugsichtungen.Webclients.SightingService;

namespace Zugsichtungen.SignalR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : AppBase
    {   

        protected override void ConfigureSpecificServices(IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7046/SignalRHub") 
                    .WithAutomaticReconnect()                          
                    .Build();
                connection.StartAsync().ConfigureAwait(false);

                return connection;
            });

            var useOData = false; // Ersetzen durch appsettings.json

            if (useOData)
            {
                services.AddHttpClient<ISightingService, SightingODataService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7046/");
                });
            }
            else
            {
                services.AddHttpClient<ISightingService, SightingApiService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7046/");
                });
            }

            services.AddSingleton<ISignalRClient, SignalRClient>();
            services.AddSingleton<SightingOverviewTabViewModelBase, SightingOverviewSignalRTabViewModel>();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var signalRClient = ServiceProvider.GetRequiredService<ISignalRClient>();
            await signalRClient.StopAsync();
            await signalRClient.DisposeAsync();
        }
    }
}
