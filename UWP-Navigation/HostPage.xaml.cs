using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UWP_Navigation.Helpers;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWP_Navigation
{
    public sealed partial class HostPage : Page, IHostPage, NavigationService.IOnBackRequestedListener, INotifyPropertyChanged
    {
        private const int DefaultFrameCacheSize = 5;

        #region Constructor

        private NavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MenuItem> NavigationMenuItems { get; set; }
        public Platform DevicePlatform
        {
            get
            {
                if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                    return Platform.WindowsPhone;
                else
                    return Platform.Windows;
            }
        }

        private NavigationService NavigationService
        {
            get
            {
                if (this._navigationService == null)
                    this._navigationService = new NavigationService(NavigationFrame);
                return this._navigationService;
            }
        }

        public HostPage()
        {
            NavigationMenuItems = new ObservableCollection<MenuItem>();
            DataContext = this;
            this.InitializeComponent();
            NavigationService.OnBackRequestedListener = this;
            Loaded += OnHostPageLoaded;
        }

        #endregion

        #region Private methods

        private void OnHostPageLoaded(object sender, RoutedEventArgs e)
        {
            NavigationFrame.CacheSize = DefaultFrameCacheSize;
            NavigateToPage(typeof(PageOne));
            LoadNavigationMenu();
        }

        private void LoadNavigationMenu()
        {
            NavigationMenuItems.Add(new MenuItem(Symbol.Admin, "PageOne"));
            NavigationMenuItems.Add(new MenuItem(Symbol.CellPhone, "PageTwo"));
            NavigationMenuItems.Add(new MenuItem(Symbol.Help, "PageThree"));
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            NotifyPropertyChanged(nameof(DevicePlatform));
        }

        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            Frame frame = sender as Frame;
            if (frame == null)
                return;
            if (frame.BackStackDepth == frame.CacheSize)
                frame.CacheSize++;
        }

        private void OnListBoxTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            var index = listBox.SelectedIndex;
            var pageType = GetPageType(index);

            if (pageType != null)
            {
                TogglePaneButton.IsChecked = false;
                NavigateToPage(pageType);
            }
        }

        private static Type GetPageType(int index)
        {
            Type pageType = null;
            switch (index)
            {
                case 0:
                    pageType = typeof(PageOne);
                    break;
                case 1:
                    pageType = typeof(PageTwo);
                    break;
                case 2:
                    pageType = typeof(PageThree);
                    break;
            }
            return pageType;
        }

        private void ClearFrameBackStack()
        {
            var backStack = NavigationFrame.BackStack;
            for (int i = backStack.Count - 1; i > 0; i--)
            {
                backStack.RemoveAt(i);
            }
            NavigationFrame.CacheSize = 0;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Public methods

        public bool OnBackRequest()
        {
            return false;
        }

        public void UpdateTitle(string title)
        {
            PageTitle.Text = title;
        }

        public void NavigateToPage(Type pageType, Dictionary<string, object> parameters = null)
        {
            if (pageType == NavigationFrame.CurrentSourcePageType)
                return;

            if (pageType == typeof(PageOne) && NavigationFrame.BackStackDepth > 0)
            {
                NavigateToHome();
                return;
            }

            if (parameters == null)
                parameters = new Dictionary<string, object>();
            parameters.Add(AppConstants.AppHostPageKey, this);
            NavigationService.NavigateToPage(pageType, parameters);
        }

        public void NavigateBack()
        {
            NavigationService.RequestBackNavigation();
        }

        public void NavigateToHome()
        {
            ClearFrameBackStack();
            NavigateBack();
        }

        #endregion
    }
}
