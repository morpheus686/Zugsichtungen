using System.Windows;
using Zugsichtungen.UI.Controls.CustomControls;
using Zugsichtungen.ViewModels.Enumerations;

namespace Zugsichtungen.DialogViews
{
    /// <summary>
    /// Interaktionslogik für IndeterminateDialogView.xaml
    /// </summary>
    public partial class IndeterminateDialogView : DialogView
    {
        public IndeterminateDialogView()
        {
            InitializeComponent();
        }

        public IndeterminateState State
        {
            get { return (IndeterminateState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(IndeterminateState), typeof(IndeterminateDialogView), new PropertyMetadata(IndeterminateState.Working));
    }
}
