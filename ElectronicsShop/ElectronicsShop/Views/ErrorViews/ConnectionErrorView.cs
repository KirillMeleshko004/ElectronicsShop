using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace ElectronicsShop.Views.ErrorViews
{
    public class ConnectionErrorView
    {
        public static async void ShowErrorMessage()
        {
            await Toast.Make("No connection", ToastDuration.Short).Show();
        }
    }
}
