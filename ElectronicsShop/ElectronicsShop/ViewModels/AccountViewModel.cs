using ElectronicsShop.Views;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class AccountViewModel : BaseViewModel
    {
        [ObservableProperty]
        string userName;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool isSignedIn;

        public bool IsNotSignedIn => !isSignedIn;

        public AccountViewModel()
        {
            IsSignedIn = App.UserAccount.IsSignedIn;
            if (IsSignedIn) UserName = App.UserAccount.Login;

            App.UserAccount.AccountStateChanged += AccountStateUpdated;
        }

        void AccountStateUpdated(object sender, EventArgs e)
        {
            IsSignedIn = App.UserAccount.IsSignedIn;
            UserName = App.UserAccount.Login;
        }

        [RelayCommand]
        async void SignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
        }
    }
}
