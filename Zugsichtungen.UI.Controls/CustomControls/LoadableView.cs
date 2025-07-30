using System.Windows.Controls;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.UI.Controls.CustomControls
{
    public class LoadableView : ContentControl
    {
        public LoadableView()
        {
            this.Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is LoadableViewModel lvm)
            {
                Task loadTask = lvm.Initialize();

                if (loadTask.IsCompleted)
                {
                    return;
                }

                await loadTask;
            }
        }
    }
}
