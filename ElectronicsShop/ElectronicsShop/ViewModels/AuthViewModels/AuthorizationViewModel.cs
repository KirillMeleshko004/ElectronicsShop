namespace ElectronicsShop.ViewModels.AuthViewModels
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
            if(!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                IsBusy = false;
                return;
            }
            AccountInfo accountInfo = await authorizationService.AuthUserAsync(Login, Password);
            if (accountInfo.ErrorMessage == AccountErrorMessages.SUCCESS)
            {
                IsSuccessful = true;
                IsFailed = false;
                App.UserName = accountInfo.Login;
                if (accountInfo.Role == Roles.Admin)
                    App.Current.MainPage = new AdminShell();
                else
                    App.Current.MainPage = new AppShell();
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
            await Shell.Current.GoToAsync(nameof(RegistrationView));
            IsBusy = false;
        }
    }
}
