using ElectronicsShop.Services;

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

        public Command SignInCommand { get; }
        public AuthorizationViewModel(AuthorizationService authorizationService, Account account)
        {
            this.authorizationService = authorizationService;
            this.account = account;
            Title = "Authorization";
            SignInCommand = new Command(async () => await DoSingInAsync());
        }
        async Task DoSingInAsync()
        {
            IsBusy = true;
            isSuccessful = await authorizationService.DoAuthorizationAsync(Login, Password);
            if (isSuccessful)
            {
                account.Login = Login;
                IsLoginOrPasswordWrong = false;
            }
            else
            {
                ErrorMessage = "Incorrect login or password!";
                IsLoginOrPasswordWrong = true;
                Password = string.Empty;
            }
            IsBusy = false;
        }
    }
}
