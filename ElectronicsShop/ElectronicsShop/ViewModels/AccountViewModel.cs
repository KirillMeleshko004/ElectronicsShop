namespace ElectronicsShop.ViewModels
{
    public partial class AccountViewModel : BaseViewModel
    {
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
            _passwordChangingService = passwordChangingService;

            Refresh();
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


        [RelayCommand]
        async void SignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
        }

        [RelayCommand]
        public async Task CahngePassword()
        {
            IsBusy = true;
            AccountInfo accountInfo = await _passwordChangingService.ChangePasswordAsync(App.UserAccount.UserName, OldPassword, NewPassword, RepeatPassword);
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

            IsBusy = false;
        }
        public void Refresh()
        {
            UserName = App.UserAccount.UserName;

            IsSuccessful = false;
            IsFailed = false;
        }
    }
}
