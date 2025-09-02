using CommunityToolkit.Maui.Alerts;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.MAUI.Services
{
    public class SnackbarService : ISnackbarService
    {
        public object SnackbarMessageQueue => throw new NotImplementedException();

        public void Show(string message)
        {
            var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long);
            toast.Show();
        }
    }
}
