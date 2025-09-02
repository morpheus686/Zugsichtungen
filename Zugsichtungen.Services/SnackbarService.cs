using MaterialDesignThemes.Wpf;
using Zugsichtungen.Abstractions.Services;

namespace Zugsichtungen.Services
{
    public class SnackbarService : ISnackbarService
    {
        public object SnackbarMessageQueue => snackbarMessageQueue;
        private readonly SnackbarMessageQueue snackbarMessageQueue;

        public SnackbarService()
        {
            snackbarMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(5));
        }

        public void Show(string message)
        {
            snackbarMessageQueue.Enqueue(message);
        }
    }
}
