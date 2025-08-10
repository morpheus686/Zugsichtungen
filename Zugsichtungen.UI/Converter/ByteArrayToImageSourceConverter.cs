using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Zugsichtungen.UI.Converter
{
    [ValueConversion(typeof(byte[]), typeof(BitmapImage))]
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not byte[] bytes || bytes.Length == 0)
                return null;

            try
            {
                using var ms = new MemoryStream(bytes);

                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // WICHTIG: Stream danach freigeben
                image.DecodePixelWidth = 600;
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze(); // WICHTIG für Thread-Sicherheit in WPF

                return image;
            }
            catch
            {
                return null; // Im Zweifel einfach kein Bild anzeigen
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Rückkonvertierung ist hier meistens nicht nötig
            throw new NotImplementedException();
        }
    }
}
