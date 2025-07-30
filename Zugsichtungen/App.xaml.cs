using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Models;
using Zugsichtungen.Services;
using Zugsichtungen.ViewModel;
using Zugsichtungen.ViewModel.DialogViewModel;
using Zugsichtungen.Views;

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

            services.AddScoped<IDataService, EfDataService>();
            services.AddScoped<ISichtungService, SichtungService>();

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
