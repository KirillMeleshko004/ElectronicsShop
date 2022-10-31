using ElectronicsShop.Services;
using ElectronicsShop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        AccountCreationService accountCreationService;

        [ObservableProperty]
        public string login;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string repeatPassword;

        [ObservableProperty]
        bool isSuccessful;

        [ObservableProperty]
        bool isLoginWrong;

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
            if (accountInfo.Error == AccountInfo.Errors.NoErrors) isSuccessful = true;
            if (isSuccessful)
            {
                IsLoginWrong = false;
            }
            else
            {
                ErrorMessage = accountInfo.ErrorMessage;
                IsLoginWrong = true;
            }
            IsBusy = false;
        }
    }
}
