using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class AppShellViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool _isSignedIn;
        public bool IsNotSignedIn => !_isSignedIn;

        public AppShellViewModel()
        {
            App.UserAccount.PropertyChanged += AccountStateUpdated;
        }

        [RelayCommand]
        void SignOut()
        {
            IsSignedIn = false;
            App.UserAccount.Login = null;
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
            this.IsSignedIn = !App.UserAccount.IsSignedIn;
            this.IsSignedIn = App.UserAccount.IsSignedIn;
        }

    }
}
