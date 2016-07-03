using UWP_Navigation.Helpers;

namespace UWP_Navigation
{
    public sealed partial class PageThree : BasePage
    {
        public PageThree()
        {
            this.InitializeComponent();
        }

        protected override string GetTitle()
        {
            return "Page Three";
        }

        private void OnGoBackClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateBack();
        }

        private void OnGoToPageThreeClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateTo(typeof(PageThree));
        }

        private void OnGoToHomeClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateToHome();
        }

        private void OnGoToPageTwoClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateTo(typeof(PageTwo));
        }
    }
}
