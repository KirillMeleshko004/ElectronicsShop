namespace ElectronicsShop.ViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        readonly AccountCreationService accountCreationService;

        [ObservableProperty]
        public string login;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string repeatPassword;

        [ObservableProperty]
        string errorMessage;
        public RegistrationViewModel(AccountCreationService accountCreationService)
        {
            this.accountCreationService = accountCreationService;
            Title = "Authorization";
        }

        [RelayCommand]
        async Task CreateAccount()
        {
            IsBusy = true;
            AccountInfo accountInfo = await accountCreationService.CreateAccountAsync(Login, Password, RepeatPassword);
            if (accountInfo.ErrorMessage != AccountErrorMessages.SUCCESS)
            {
                IsSuccessful = false;
                ErrorMessage = accountInfo.ErrorMessage;
            }
            else IsSuccessful = true;

            IsBusy = false;
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
