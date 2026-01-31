using Microsoft.Extensions.DependencyInjection;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ApplicationBase;
using Zugsichtungen.Webclients.GalleryServices;
using Zugsichtungen.Webclients.SightingService;

namespace Zugsichtungen.Rest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : AppBase
    {
        private const string UriString = "http://localhost:7046/";

        protected override void ConfigureSpecificServices(IServiceCollection services)
        {
            var useOData = false; // Ersetzen durch appsettings.json

            if (useOData)
            {
                services.AddHttpClient<ISightingService, SightingODataService>(client =>
                {
                    client.BaseAddress = new Uri(UriString);
                });

                services.AddHttpClient<IGalleryService, GalleryODataService>(client =>
                {
                    client.BaseAddress = new Uri(UriString);
                });
            }
            else
            {
                services.AddHttpClient<ISightingService, SightingApiService>(client =>
                {
                    client.BaseAddress = new Uri(UriString);
                });

                services.AddHttpClient<IGalleryService, GalleryApiService>(client =>
                {
                    client.BaseAddress = new Uri(UriString);
                });
            }
        }
    }
}
