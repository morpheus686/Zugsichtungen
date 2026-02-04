using Microsoft.Extensions.DependencyInjection;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ApplicationBase;
using Zugsichtungen.Webclients.GalleryServices;
using Zugsichtungen.Webclients.SightingService;

namespace Zugsichtungen.OData
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : AppBase
    {
        private const string UriString = "http://localhost:7046/";

        protected override void ConfigureSpecificServices(IServiceCollection services)
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
    }
}
