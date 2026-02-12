using Zugsichtungen.UI.Controls.CustomControls;
using Zugsichtungen.Wpf.ViewModels.ItemViewModel;

namespace Zugsichtungen.UI.Views.DialogViews
{
    /// <summary>
    /// Interaktionslogik für ShowPictureDialogView.xaml
    /// </summary>
    public partial class ShowPictureDialogView : DialogView
    {
        public ShowPictureDialogView()
        {
            InitializeComponent();
        }

        private void Image_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (DataContext is GalleryItemWpfViewModel vm)
            {
                vm.ZoomCommand.Execute(e.Delta);
            }
        }
    }
}
