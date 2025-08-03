using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;
using Zugsichtungen.Infrastructure.SQLite.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;
using Zugsichtungen.Infrastructure.SQLServer.Services;
using Zugsichtungen.Services;
using Zugsichtungen.UI.Views;
using Zugsichtungen.ViewModels;
using Zugsichtungen.ViewModels.DialogViewModels;

namespace Zugsichtungen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; private set; } = default!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);

            Services = services.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            switch (configuration["DatabaseProvider"])
            {
                case "SQLite":
                    services.AddDbContext<ZugbeobachtungenContext>(options =>
                    {
                        var connectionString = configuration.GetConnectionString("SQLiteConnection");
                        options.UseSqlite(connectionString);
                    });

                    services.AddScoped<IDataService, SQLiteDataService>();
                    break;
                case "SQLServer":
                    services.AddDbContext<TrainspottingContext>(options =>
                    {
                        var connectionString = configuration.GetConnectionString("SQLServerConnection");
                        options.UseSqlServer(connectionString);
                    });

                    services.AddScoped<IDataService, SqlServerDataService>();
                    break;
                default:
                    throw new ApplicationException("Keine gültige Datenbank konfiguriert!");
            }

            services.AddLogging(logging =>
            {
                logging.AddConsole();
            });

            var provider = services.BuildServiceProvider();

            services.AddAutoMapper(config =>
            {
                config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
                config.ConstructServicesUsing(provider.GetService);
            });

            services.AddScoped<ISightingService, SightingService>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<AddSichtungDialogViewModel>();
        }
    }
}
