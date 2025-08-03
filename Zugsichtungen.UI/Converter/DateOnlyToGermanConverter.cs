using System.Globalization;
using System.Windows.Data;

namespace Zugsichtungen.UI.Converter
{    
    public class DateOnlyToGermanDateConverter : IValueConverter
    {
        // Wandelt DateOnly? → string (z. B. "24.07.2025")
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly date)
            {
                return date.ToString("dd.MM.yyyy", CultureInfo.GetCultureInfo("de-DE"));
            }

            return string.Empty;
        }

        // Rückkonvertierung: string → DateOnly?
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && DateOnly.TryParseExact(s, "dd.MM.yyyy", CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None, out var date))
            {
                return date;
            }

            return String.Empty;
        }
    }
}

