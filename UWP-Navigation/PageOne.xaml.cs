using System;
using UWP_Navigation.Helpers;

namespace UWP_Navigation
{
    public sealed partial class PageOne : BasePage
    {
        public PageOne()
        {
            this.InitializeComponent();
        }

        protected override string GetTitle()
        {
            return "Page One";
        }

        private void OnGoToPageTwoClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateTo(typeof(PageTwo));
        }

        private void OnGoToPageThreeClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateTo(typeof(PageThree));
        }
    }
}
