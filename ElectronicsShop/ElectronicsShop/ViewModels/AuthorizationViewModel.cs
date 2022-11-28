using ElectronicsShop.Services;


namespace ElectronicsShop.ViewModels
{
    public partial class AuthorizationViewModel : BaseViewModel
    {
        readonly AuthorizationService authorizationService;

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
        async Task SignUp()
        {
            IsBusy = true;
            Login = string.Empty;
            Password = string.Empty;
            IsLoginOrPasswordWrong = false;
            IsSuccessful = false;
            await Shell.Current.GoToAsync(nameof(RegistrationView), true);
            IsBusy = false;
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..",
            new Dictionary<string, object>
            {
                ["IsSignedIn"] = isSuccessful
            });
        }
    }
}
