namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class AccountViewModel : BaseViewModel, IRefreshableAsync
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

            RefreshAsync();
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
        public async Task CahngePassword()
        {
            IsBusy = true;
            AccountInfo accountInfo = await _passwordChangingService.ChangePasswordAsync(App.UserName, OldPassword, NewPassword, RepeatPassword);
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
        public void RefreshAsync()
        {
            UserName = App.UserName;

            IsSuccessful = false;
            IsFailed = false;
        }
    }
}
