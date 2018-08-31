using Xamarin.Forms;

namespace AirMVVM.Xamarin.Forms.Controls
{
    public class TemplateContentView : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(TemplateContentView), propertyChanged: OnItemOrSourceChanged);
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create("ItemSource", typeof(object), typeof(TemplateContentView), propertyChanged: OnItemOrSourceChanged);

        /// <summary>
        /// A <see cref="DataTemplate"/> is used to render the view. This property is bindable.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }

        /// <summary>
        /// <see cref="ItemSource"/> is rendered on view. This property is bindable.
        /// </summary>
        public object ItemSource
        {
            get
            {
                return GetValue(ItemSourceProperty);
            }
            set
            {
                SetValue(ItemSourceProperty, value);
            }
        }

        private static void OnItemOrSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var self = (TemplateContentView)bindable;

            var template = self.ItemTemplate;
            if (template is DataTemplateSelector)
            {
                var templateSelector = template as DataTemplateSelector;
                template = templateSelector.SelectTemplate(self.ItemSource, bindable);
            }
            if (template != null)
            {
                var content = template.CreateContent();
                View view;
                if (content is ViewCell)
                {
                    var viewCell = content as ViewCell;
                    view = viewCell.View;
                }
                else
                {
                    view = (View)content;
                }
                view.BindingContext = self.ItemSource;
                self.Content = view;
            }
            else
            {
                self.Content = null;
            }
        }
    }
}
