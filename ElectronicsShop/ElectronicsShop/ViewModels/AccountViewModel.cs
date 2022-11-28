using ElectronicsShop.Models;
using ElectronicsShop.Services;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class AccountViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool isSignedIn;
        public bool IsNotSignedIn => !isSignedIn;


        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string oldPassword;

        [ObservableProperty]
        string newPassword;

        [ObservableProperty]
        string repeatPassword;

        [ObservableProperty]
        string errorMessage;


        readonly PasswordChangingService _passwordChangingService;
        public AccountViewModel(PasswordChangingService passwordChangingService)
        {
            IsSignedIn = App.UserAccount.IsSignedIn;
            if (IsSignedIn) UserName = App.UserAccount.Login;

            App.UserAccount.PropertyChanged += AccountStateUpdated;
            _passwordChangingService = passwordChangingService;
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
            if (e.PropertyName != nameof(Account.IsSignedIn)) return;
            this.IsSignedIn = !App.UserAccount.IsSignedIn;
            this.IsSignedIn = App.UserAccount.IsSignedIn;
            if (IsSignedIn) this.UserName = App.UserAccount.Login;
        }

        [RelayCommand]
        async void SignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
        }

        [RelayCommand]
        public async Task CahngePassword()
        {
            IsBusy = true;
            AccountInfo accountInfo = await _passwordChangingService.ChangePasswordAsync(App.UserAccount.Login, OldPassword, NewPassword, RepeatPassword);
            if (accountInfo.ErrorMessage == AccountErrorMessages.SUCCESS)
            {
                IsSuccessful = true;
                IsFailed = false;
            }
            else
            {
                IsSuccessful = false;
                ErrorMessage = accountInfo.ErrorMessage;
                IsFailed = true;
            }
            OldPassword = string.Empty;
            NewPassword = string.Empty;
            RepeatPassword = string.Empty;


            await Task.Delay(5000);
            IsSuccessful = false;
            IsFailed = false;

            IsBusy = false;
        }
        public void Refresh()
        {
            IsSignedIn = App.UserAccount.IsSignedIn;
            if (IsSignedIn) UserName = App.UserAccount.Login;

            App.UserAccount.PropertyChanged += AccountStateUpdated;
        }
    }
}
