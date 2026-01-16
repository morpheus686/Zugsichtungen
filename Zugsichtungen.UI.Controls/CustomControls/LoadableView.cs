using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Zugsichtungen.Abstractions.Interfaces;

namespace Zugsichtungen.UI.Controls.CustomControls
{
    public class LoadableView : ContentControl
    {
        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(LoadableView), new PropertyMetadata(false));

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public LoadableView()
        {
            this.DataContextChanged += LoadableView_DataContextChanged;
        }

        private void LoadableView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ILoadable loadable)
            {
                BindingOperations.SetBinding(
                    this,
                    IsLoadingProperty,
                    new Binding(nameof(loadable.IsInitializing))
                    {
                        Source = loadable,
                        Mode = BindingMode.OneWay
                    });
            }
        }
    }
}
