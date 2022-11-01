using ElectronicsShop.Views;

namespace ElectronicsShop.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [RelayCommand]
        Task LogOut()
        {
            App.UserAccount = null;
            return Shell.Current.GoToAsync($"//{nameof(AuthorizationView)}");
        }
    }
}
