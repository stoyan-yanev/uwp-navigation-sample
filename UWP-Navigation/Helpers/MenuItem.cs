using Windows.UI.Xaml.Controls;

namespace UWP_Navigation.Helpers
{
    public class MenuItem
    {
        public Symbol ItemSymbol { get; set; }
        public string ItemName { get; set; }

        public MenuItem(Symbol symbol, string name)
        {
            ItemSymbol = symbol;
            ItemName = name;
        }
    }
}
