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
            AccountInfo accountInfo = await authorizationService.AuthUserAsync(Login, Password);
            if (accountInfo.ErrorMessage == AccountErrorMessages.SUCCESS)
            {
                IsSuccessful = true;
                App.UserAccount.UserName = Login;
                App.UserAccount.IsSignedIn = true;
                IsFailed = false;
                await Shell.Current.GoToAsync($"..",
                new Dictionary<string, object>
                {
                    ["IsSignedIn"] = IsSuccessful
                });
            }
            else
            {
                IsSuccessful = false;
                ErrorMessage = accountInfo.ErrorMessage;
                IsFailed = true;
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
            IsFailed = false;
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
                ["IsSignedIn"] = IsSuccessful
            });
        }
    }
}
