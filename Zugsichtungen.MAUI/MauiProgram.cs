using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Zugsichtungen.Abstractions.Interfaces;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Infrastructure.Services;
using Zugsichtungen.Infrastructure.SQLite.Models;
using Zugsichtungen.Infrastructure.SQLite.Repositories;
using Zugsichtungen.Infrastructure.SQLite.Services;
using Zugsichtungen.MAUI.Services;
using Zugsichtungen.ViewModels.DialogViewModels;
using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var dbPath = PrepareDatabase();
            var sqliteConnectionString = $"Data Source={dbPath}";

            builder.Services.AddDbContext<ZugbeobachtungenContext>(options =>
            {
                options.UseSqlite(sqliteConnectionString);
            });

            builder.Services.AddScoped<IDataService, SQLiteDataService>();
            builder.Services.AddScoped<IImageRepository, SQLiteImageRepository>(sp =>
            {
                return new SQLiteImageRepository(sqliteConnectionString);
            });

            builder.Services.AddLogging(logging =>
            {
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Information);
            });

            builder.Services.AddAutoMapper(config => config.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            builder.Services.AddScoped<ISightingService, SightingService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<SightingOverviewTabViewModel>();
            builder.Services.AddSingleton<GalleryTabViewModel>();

            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddTransient<AddSichtungDialogViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static string PrepareDatabase()
        {
            var dbFileName = "zugbeobachtungen.db";
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);

            if (!File.Exists(dbPath))
            {
                using var stream = FileSystem.OpenAppPackageFileAsync(dbFileName).GetAwaiter().GetResult();
                using var newStream = File.Create(dbPath);
                stream.CopyTo(newStream);
            }

            return dbPath;
        }
    }
}
