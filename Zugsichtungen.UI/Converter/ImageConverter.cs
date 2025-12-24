using System.Globalization;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Zugsichtungen.UI.Converter
{
    /// <summary>
    /// Konvertiert einen Dateipfad oder ein byte[] in ein BitmapImage für die Anzeige in der UI.
    /// </summary>
    public class ImageConverter : IValueConverter
    {
        private const int DefaultPixelWidth = 600;
        /// <summary>
        /// Optional: gewünschte Pixelbreite für Vorschaubilder.
        /// </summary>
        public int? PixelWidth { get; set; } = DefaultPixelWidth;

        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return CreateBitmapImage(value);
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lädt ein BitmapImage entweder aus Pfad oder aus byte[].
        /// </summary>
        private BitmapImage? CreateBitmapImage(object? source)
        {
            if (source == null)
                return null;

            try
            {
                byte[] bytes;

                switch (source)
                {
                    case string path when File.Exists(path):
                        bytes = File.ReadAllBytes(path);
                        break;

                    case byte[] b when b.Length > 0:
                        bytes = b;
                        break;

                    default:
                        return null;
                }

                using var ms = new MemoryStream(bytes);
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Stream danach freigeben
                if (PixelWidth.HasValue)
                    bitmap.DecodePixelWidth = PixelWidth.Value;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze(); // optional: Thread-Safe

                return bitmap;
            }
            catch
            {
                return null; // Im Fehlerfall kein Bild anzeigen
            }
        }
    }
}