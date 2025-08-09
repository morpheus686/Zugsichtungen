using System.Globalization;
using System.Windows.Data;

namespace Zugsichtungen.UI.Converter
{
    public class ReferenceEqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ReferenceEquals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return parameter!;
            return Binding.DoNothing;
        }
    }
}
