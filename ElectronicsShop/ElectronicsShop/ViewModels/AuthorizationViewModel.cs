using ElectronicsShop.Services;
using ElectronicsShop.Views;

namespace ElectronicsShop.ViewModels
{
    public partial class AuthorizationViewModel : BaseViewModel
    {
        AuthorizationService authorizationService;

        [ObservableProperty]
        string login;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool isSuccessful;

        [ObservableProperty]
        bool isLoginOrPasswordWrong;

        [ObservableProperty]
        string errorMessage;

        public AuthorizationViewModel(AuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
            Title = "Authorization";
        }

        [RelayCommand]
        public async Task SignIn()
        {
            IsBusy = true;
            AccountInfo accountInfo = await authorizationService.DoAuthorizationAsync(Login, Password);
            if (accountInfo.ErrorMessage == AccountErrorMessages.SUCCESS) isSuccessful = true;
            else isSuccessful = false;
            if (isSuccessful)
            {
                App.UserAccount.Login = Login;
                App.UserAccount.IsSignedIn = true;
                IsLoginOrPasswordWrong = false;
                await Shell.Current.GoToAsync($"..",
                new Dictionary<string, object>
                {
                    ["IsSignedIn"] = isSuccessful
                });
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
        Task SignUp()
        {       
            Login = string.Empty;
            Password = string.Empty;
            IsLoginOrPasswordWrong = false;
            IsSuccessful = false;
            return Shell.Current.GoToAsync(nameof(RegistrationView), true);
        }
    }
}
