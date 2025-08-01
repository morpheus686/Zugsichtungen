using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Mapping;
using Zugsichtungen.Infrastructure.Models;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SqlServerModels;
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

            services.AddDbContext<ZugbeobachtungenContext>(optionsAction =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsAction.UseSqlite(connectionString);
            });

            services.AddDbContext<TrainspottingContext>();

            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging.AddConsole();
            });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SightingProfile>();
                cfg.AddProfile<SightingViewEntryProfile>();
                cfg.AddProfile<VehicleViewEntryProfile>();
                cfg.AddProfile<ContextProfile>();

            }, loggerFactory);

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IDataService, SQLiteDataService>();
            //services.AddScoped<IDataService, SqlServerDataService>();
            services.AddScoped<ISightingService, SightingService>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<AddSichtungDialogViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
