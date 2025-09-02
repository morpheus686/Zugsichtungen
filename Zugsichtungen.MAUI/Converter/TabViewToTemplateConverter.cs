using Zugsichtungen.ViewModels.TabViewModels;

namespace Zugsichtungen.MAUI.Converter
{
    public class TabViewToTemplateConverter : IValueConverter
    {
        public DataTemplate OverviewTemplate { get; set; } = null!;
        public DataTemplate GalleryTemplate { get; set; } = null!;

        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            BindableObject? view = null;

            switch (value)
            {
                case SightingOverviewTabViewModelBase overview:
                    view = OverviewTemplate.CreateContent() as BindableObject;
                    if (view != null) view.BindingContext = overview;
                    break;

                case GalleryTabViewModel gallery:
                    view = GalleryTemplate.CreateContent() as BindableObject;
                    if (view != null) view.BindingContext = gallery;
                    break;

                default:
                    return null;
            }

            return view;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}