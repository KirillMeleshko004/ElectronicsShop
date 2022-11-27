using System.ComponentModel;

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

            App.UserAccount.PropertyChanged += AccountStateUpdated;
        }

        [RelayCommand]
        async void GoToOrders()
        {
            await Shell.Current.GoToAsync($"{nameof(OrdersView)}",
                new Dictionary<string, object>
                {
                    ["UserName"] = userName
                });
        }

        void AccountStateUpdated(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Account.IsSignedIn)) return;
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
