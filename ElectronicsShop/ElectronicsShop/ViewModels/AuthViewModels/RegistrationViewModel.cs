using ElectronicsShop.Models;
using ElectronicsShop.Services.AccountServices;
using ElectronicsShop.Views.ErrorViews;

namespace ElectronicsShop.ViewModels.AuthViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        readonly AccountCreationService _accountCreationService;

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
            this._accountCreationService = accountCreationService;
            Title = "Authorization";
        }

        [RelayCommand]
        async Task CreateAccount()
        {
            IsBusy = true;

            try
            {
                AccountInfo accountInfo = await _accountCreationService.RegisterUserAsync(Login, Password, RepeatPassword);
                if (accountInfo.ErrorMessage != AccountErrorMessages.SUCCESS)
                {
                    IsSuccessful = false;
                    ErrorMessage = accountInfo.ErrorMessage;
                }
                else IsSuccessful = true;
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
                ErrorMessage = null;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
