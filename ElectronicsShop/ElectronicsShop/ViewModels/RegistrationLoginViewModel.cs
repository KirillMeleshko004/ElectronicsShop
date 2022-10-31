using ElectronicsShop.Services;
using ElectronicsShop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public partial class RegistrationLoginViewModel : BaseViewModel
    {
        LoginCheckService loginCheckService;

        [ObservableProperty]
        public string login;

        [ObservableProperty]
        bool isSuccessful;

        [ObservableProperty]
        bool isLoginWrong;

        [ObservableProperty]
        string errorMessage;
        public RegistrationLoginViewModel(LoginCheckService loginCheckService)
        {
            this.loginCheckService = loginCheckService;
            Title = "Authorization";
        }
        [RelayCommand]
        async Task LoginConfirmation()
        {
            IsBusy = true;
            AccountInfo accountInfo = await loginCheckService.CheckLoginAsync(Login);
            if (accountInfo.Error == AccountInfo.Errors.NoErrors) isSuccessful = true;
            if (isSuccessful)
            {
                IsLoginWrong = false;
                await Shell.Current.GoToAsync(nameof(RegistrationViewPasswordPage), true,
                    new Dictionary<string, object>()
                    {
                    { "Login", accountInfo.Login }
                    });
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
