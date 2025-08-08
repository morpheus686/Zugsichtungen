using System.Diagnostics;
using Zugsichtungen.UI.Controls.CustomControls;

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
    }
}
