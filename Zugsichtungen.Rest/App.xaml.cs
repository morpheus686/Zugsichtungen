using Microsoft.Extensions.DependencyInjection;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ApplicationBase;
using Zugsichtungen.Webclients.SightingService;

namespace Zugsichtungen.Rest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : AppBase
    {
        protected override void ConfigureSpecificServices(IServiceCollection services)
        {
            var useOData = true; // Ersetzen durch appsettings.json

            if (useOData)
            {
                services.AddHttpClient<ISightingService, SightingODataService>(client =>
                {
                    client.BaseAddress = new Uri("http://localhost:7046/");
                });
            }
            else
            {
                services.AddHttpClient<ISightingService, SightingApiService>(client =>
                {
                    client.BaseAddress = new Uri("http://localhost:7046/");
                });
            }
        }
    }
}
