using UWP_Navigation.Helpers;

namespace UWP_Navigation
{
    public sealed partial class PageTwo : BasePage
    {
        public PageTwo()
        {
            this.InitializeComponent();
        }

        protected override string GetTitle()
        {
            return "Page Two";
        }

        private void OnGoBackClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateBack();
        }

        private void OnGoToPageThreeClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateTo(typeof(PageThree));
        }
    }
}
