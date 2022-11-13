using ElectronicsShop.Views;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class AppShellViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool _isSignedIn;
        public bool IsNotSignedIn => !_isSignedIn;

        [RelayCommand]
        void LogOut()
        {
            IsSignedIn = false;
            App.UserAccount = null;
        }


        [RelayCommand]
        async void SignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
        }
    }
}
