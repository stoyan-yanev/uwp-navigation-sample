using System;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace UWP_Navigation.Helpers
{
    public class PlatformVisibilityConverter : DependencyObject, IValueConverter
    {
        public bool CanGoBack
        {
            get { return (bool)GetValue(CanGoBackProperty); }
            set { SetValue(CanGoBackProperty, value); }
        }

        public static readonly DependencyProperty CanGoBackProperty =
            DependencyProperty.Register(nameof(CanGoBack), typeof(bool), typeof(PlatformVisibilityConverter), new PropertyMetadata(false));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var platform = (Platform)value;
            return CanGoBack && platform != Platform.WindowsPhone ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
