using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Rest.Services;
using Zugsichtungen.Services;
using Zugsichtungen.UI.Views;
using Zugsichtungen.ViewModels;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.Rest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Information);
            });

            services.AddAutoMapper(config => config.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<SightingOverviewTabViewModel>();
            services.AddSingleton<GalleryTabViewModel>();

            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<AddSichtungDialogViewModel>();

            services.AddHttpClient<ISightingService, SightingApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7046/");
            });
        }
    }
}
