using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UWP_Navigation.Helpers
{
    public abstract class BasePage : Page
    {
        #region Constructor

        private IHostPage hostPage;

        public BasePage()
        {
            SetUpPageAnimation();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            Loaded += OnBasePageLoaded;
        }

        #endregion

        #region Private methods

        private void OnBasePageLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.hostPage.UpdateTitle(GetTitle());
        }

        private void GetParameters(Dictionary<string, object> parameters)
        {
            this.hostPage = parameters[AppConstants.AppHostPageKey] as IHostPage;
        }

        #endregion

        #region Protected methods

        protected virtual void SetUpPageAnimation()
        {
            TransitionCollection collection = new TransitionCollection();
            NavigationThemeTransition theme = new NavigationThemeTransition();

            var info = new DrillInNavigationTransitionInfo();

            theme.DefaultNavigationTransitionInfo = info;
            collection.Add(theme);
            this.Transitions = collection;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameters = e.Parameter as Dictionary<string, object>;
            GetParameters(parameters);
        }

        protected void NavigateTo(Type pageType, Dictionary<string, object> parameters = null)
        {
            this.hostPage.NavigateToPage(pageType, parameters);
        }

        protected void NavigateBack()
        {
            this.hostPage.NavigateBack();
        }

        protected void NavigateToHome()
        {
            this.hostPage.NavigateToHome();
        }

        #endregion

        #region Abstract methods

        protected abstract string GetTitle();

        #endregion
    }
}
