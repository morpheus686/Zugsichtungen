using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Zugsichtungen.UI.Views;

namespace Zugsichtungen.ApplicationBase
{
    public abstract class AppBase : Application
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

        protected abstract void ConfigureServices(IServiceCollection services);
    }
}
