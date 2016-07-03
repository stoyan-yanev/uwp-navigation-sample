using System;
using System.Collections.Generic;

namespace UWP_Navigation.Helpers
{
    public interface IHostPage
    {
        void UpdateTitle(string title);

        void NavigateToPage(Type pageType, Dictionary<string, object> parameters = null);

        void NavigateBack();

        void NavigateToHome();
    }
}
