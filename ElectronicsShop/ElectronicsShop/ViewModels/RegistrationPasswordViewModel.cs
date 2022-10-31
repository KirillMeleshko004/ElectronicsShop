using ElectronicsShop.Views;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(Login), "Login")]
    public partial class RegistrationPasswordViewModel : BaseViewModel
    {
        AccountCreationService accountCreationService;

        [ObservableProperty]
        public string login;

        [ObservableProperty]
        bool isSuccessful;

        [ObservableProperty]
        bool isLoginWrong;

        [ObservableProperty]
        string errorMessage;
        public RegistrationPasswordViewModel(AccountCreationService accountCreationService)
        {
            this.accountCreationService = accountCreationService;
            Title = "Authorization";
        }
        [RelayCommand]
        async Task AccountCreation()
        {
            IsBusy = true;
            IsBusy = false;
            await Shell.Current.GoToAsync(nameof(RegistrationViewPasswordPage), true,
                new Dictionary<string, object>()
                {
                    { "Login", Login }
                });
        }
    }
}
