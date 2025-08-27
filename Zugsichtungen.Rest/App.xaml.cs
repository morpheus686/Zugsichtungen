using Microsoft.Extensions.DependencyInjection;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ApplicationBase;
using Zugsichtungen.Rest.Services;

namespace Zugsichtungen.Rest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : AppBase
    {
        protected override void ConfigureSpecificServices(IServiceCollection services)
        {
            services.AddHttpClient<ISightingService, SightingApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7046/");
            });
        }
    }
}
