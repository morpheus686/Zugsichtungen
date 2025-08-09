using System.Globalization;
using System.Windows.Data;

namespace Zugsichtungen.UI.Converter
{
    public class ReferenceEqualityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return false;
            return ReferenceEquals(values[0], values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
            {
                // Wir wollen SelectedTab setzen, Tab2 nicht ändern
                return new object[] { parameter, Binding.DoNothing };
            }
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}
