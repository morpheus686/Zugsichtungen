using System.Globalization;

namespace Zugsichtungen.MAUI.Converter
{
    public class MultiSourceImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            switch (value)
            {
                case string path when File.Exists(path):
                    return ImageSource.FromFile(path);


                case byte[] bytes when bytes.Length > 0:
                    return ImageSource.FromStream(() => new MemoryStream(bytes));

                case Stream stream:
                    return ImageSource.FromStream(() => stream);
                default:
                    return null;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
