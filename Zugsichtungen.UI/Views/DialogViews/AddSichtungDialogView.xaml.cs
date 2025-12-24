using System.Diagnostics;
using System.Windows;
using Zugsichtungen.UI.Controls.CustomControls;
using Zugsichtungen.ViewModels.DialogViewModels;

namespace Zugsichtungen.UI.Views.DialogViews
{
    /// <summary>
    /// Interaktionslogik für AddSichtungDialogView.xaml
    /// </summary>
    public partial class AddSichtungDialogView : DialogView
    {
        public AddSichtungDialogView()
        {
            InitializeComponent();
            Debug.WriteLine($"Header: {Header}");
        }

        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            var dataContext = this.DataContext as AddSichtungDialogViewModel;

            if (dataContext == null)
            {
                e.Effects = DragDropEffects.None;
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length == 1)
                {
                    var file = files[0];
                    var isValid = dataContext.DropImageCommand.CanExecute(file);

                    if (isValid)
                    {
                        e.Effects = DragDropEffects.Copy;
                        return;
                    }
                }

                e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            var dataContext = this.DataContext as AddSichtungDialogViewModel;

            if (dataContext != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = (string[])e.Data.GetData(DataFormats.FileDrop)!;

                    if (files.Length != 1)
                    {
                        return;
                    }

                    var file = files[0];
                    dataContext.DropImageCommand.Execute(file);
                }
            }
        }
    }
}
