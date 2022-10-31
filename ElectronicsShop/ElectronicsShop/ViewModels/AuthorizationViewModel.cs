using ElectronicsShop.Services;
using ElectronicsShop.Views;

namespace ElectronicsShop.ViewModels
{
    public partial class AuthorizationViewModel : BaseViewModel
    {
        AuthorizationService authorizationService;
        Account account;

        [ObservableProperty]
        public string login;

        [ObservableProperty]
        public string password;

        [ObservableProperty]
        bool isSuccessful;

        [ObservableProperty]
        bool isLoginOrPasswordWrong;

        [ObservableProperty]
        string errorMessage;

        public AuthorizationViewModel(AuthorizationService authorizationService, Account account)
        {
            this.authorizationService = authorizationService;
            this.account = account;
            Title = "Authorization";
        }

        [RelayCommand]
        public async Task SignIn()
        {
            IsBusy = true;
            AccountInfo accountInfo = await authorizationService.DoAuthorizationAsync(Login, Password);
            if (accountInfo.Error == AccountInfo.Errors.NoErrors) isSuccessful = true;
            else isSuccessful = false;
            if (isSuccessful)
            {
                account.Login = accountInfo.Login;
                IsLoginOrPasswordWrong = false;
            }
            else
            {
                ErrorMessage = accountInfo.ErrorMessage;
                IsLoginOrPasswordWrong = true;
                Password = string.Empty;
            }
            IsBusy = false;
        }
        [RelayCommand]
        async Task SignUp()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationViewLoginPage), true);
        }
    }
}
