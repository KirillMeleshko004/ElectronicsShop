using System.ComponentModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class AppShellViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool _isSignedIn;
        public bool IsNotSignedIn => !_isSignedIn;

        [ObservableProperty]
        string userName;

        public AppShellViewModel()
        {
            App.UserAccount.PropertyChanged += AccountStateUpdated;
        }

        [RelayCommand]
        void SignOut()
        {
            IsSignedIn = false;
            UserName = null;
            App.UserAccount.UserName = null;
            App.UserAccount.IsSignedIn = false;
        }


        [RelayCommand]
        async void SignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
        }

        void AccountStateUpdated(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Account.IsSignedIn)) return;
            IsSignedIn = App.UserAccount.IsSignedIn;
            if (IsSignedIn) UserName = App.UserAccount.UserName;
        }

    }
}
