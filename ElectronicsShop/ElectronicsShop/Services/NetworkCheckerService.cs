using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace ElectronicsShop.Services
{
    public class NetworkCheckerService
    {
        public static bool CheckConnection()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }
        public static async void ShowNewtworkErrorMessage()
        {
            await Toast.Make("No connection", ToastDuration.Short).Show();
        }
    }
}
