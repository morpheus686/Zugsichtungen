namespace Zugsichtungen.MAUI.TemplateSelectors
{
    public class TabViewDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate overviewTemplate;
        private readonly DataTemplate galleryTemplate;

        public TabViewDataTemplateSelector()
        {
            overviewTemplate = (DataTemplate)Application.Current.Resources["SightingOverviewTemplate"];
            galleryTemplate = (DataTemplate)Application.Current.Resources["GalleryTemplate"];
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item switch
            {
                Zugsichtungen.ViewModels.TabViewModels.SightingOverviewTabViewModel => overviewTemplate,
                Zugsichtungen.ViewModels.TabViewModels.GalleryTabViewModel => galleryTemplate,
                _ => null
            };
        }
    }
}
