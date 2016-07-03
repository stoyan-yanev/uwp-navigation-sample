using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace UWP_Navigation.Helpers
{
    public class NavigationService
    {
        private Frame currentNavigationFrame;

        public IOnBackRequestedListener OnBackRequestedListener { get; set; }

        public NavigationService(Frame navigationFrame)
        {
            this.currentNavigationFrame = navigationFrame;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        public void NavigateToPage(Type toPage, Dictionary<string, object> parameters = null)
        {
            this.currentNavigationFrame.Navigate(toPage, parameters);
        }

        public void RequestBackNavigation()
        {
            OnBackRequested();
        }

        private void OnBackRequested(object sender = null, BackRequestedEventArgs e = null)
        {
            if (this.currentNavigationFrame == null)
                return;
            if (this.OnBackRequestedListener != null && this.OnBackRequestedListener.OnBackRequest())
            {
                if (e != null)
                    e.Handled = true;
                return;
            }
            if (this.currentNavigationFrame.CanGoBack)
            {
                if (e != null)
                    e.Handled = true;
                this.currentNavigationFrame.GoBack();
            }
        }

        public interface IOnBackRequestedListener
        {
            bool OnBackRequest();
        }
    }
}
