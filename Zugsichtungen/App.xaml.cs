using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;
using Zugsichtungen.Infrastructure.SQLite.Repositories;
using Zugsichtungen.Infrastructure.SQLite.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;
using Zugsichtungen.Infrastructure.SQLServer.Repositories;
using Zugsichtungen.Infrastructure.SQLServer.Services;
using Zugsichtungen.Services;
using Zugsichtungen.UI.Views;
using Zugsichtungen.ViewModels;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider ServiceProvider { get; private set; } = default!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
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
                    var sqliteConnectionString = configuration.GetConnectionString("SQLiteConnection");

                    services.AddDbContext<ZugbeobachtungenContext>(options =>
                    {

                        options.UseSqlite(sqliteConnectionString);
                    });

                    services.AddScoped<IDataService, SQLiteDataService>();
                    services.AddScoped<IImageRepository, SQLiteImageRepository>(sp =>
                    {
                        return new SQLiteImageRepository(sqliteConnectionString);
                    });
                    break;
                case "SQLServer":
                    var sqlServerConnectionString = configuration.GetConnectionString("SQLServerConnection");

                    services.AddDbContext<TrainspottingContext>(options =>
                    {
                        options.UseSqlServer(sqlServerConnectionString);
                    });

                    services.AddScoped<IDataService, SqlServerDataService>();
                    services.AddScoped<IImageRepository, SQLServerImageRepository>(sp =>
                    {
                        return new SQLServerImageRepository(sqlServerConnectionString);
                    });
                    break;
                default:
                    throw new ApplicationException("Keine gültige Datenbank konfiguriert!");
            }

            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Information);
            });

            services.AddAutoMapper(config => config.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddScoped<ISightingService, SightingService>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<SightingOverviewTabViewModel>();
            services.AddSingleton<GalleryTabViewModel>();

            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<AddSichtungDialogViewModel>();
        }
    }
}
