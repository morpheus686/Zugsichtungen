using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.ApplicationBase;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Helpers;
using Zugsichtungen.Infrastructure.SQLite.Models;
using Zugsichtungen.Infrastructure.SQLite.Repositories;
using Zugsichtungen.Infrastructure.SQLite.Services;
using Zugsichtungen.Infrastructure.SQLServer.Models;
using Zugsichtungen.Infrastructure.SQLServer.Repositories;
using Zugsichtungen.Infrastructure.SQLServer.Services;

namespace Zugsichtungen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : AppBase
    {

        protected override void ConfigureSpecificServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            switch (configuration["DatabaseProvider"])
            {
                case "SQLite":
                    var dbPath = SqliteHelper.CopyDatabaseIfNotExits();
                    var sqliteConnectionString = $"Data Source={dbPath}";

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

                    if (sqlServerConnectionString == null)
                    {
                        throw new ApplicationException("Connectionstring ist nicht in den Einstellungen festgelegt!");
                    }

                    services.AddScoped<IImageRepository, SQLServerImageRepository>(sp =>
                    {
                        return new SQLServerImageRepository(sqlServerConnectionString);
                    });

                    services.AddSingleton<ISightingService, SightingService>();
                    break;
                default:
                    throw new ApplicationException("Keine gültige Datenbank konfiguriert!");
            }

            services.AddSingleton<ISightingService, SightingService>();
        }
    }
}
