using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        LoginCheckService loginCheckService;
        AccountCreationService accountCreationService;

        [ObservableProperty]
        public string login;

        [ObservableProperty]
        public string password;

        [ObservableProperty]
        bool isSuccessful;

        [ObservableProperty]
        bool isLoginWrong;

        [ObservableProperty]
        string errorMessage;

        public Command ConfirmLoginCommand { get; }
        public RegistrationViewModel(LoginCheckService loginCheckService, AccountCreationService accountCreationService)
        {
            this.loginCheckService = loginCheckService;
            this.accountCreationService = accountCreationService;
            Title = "Authorization";
            ConfirmLoginCommand = new Command(async () => await ConfirmLogin());
        }
        async Task ConfirmLogin()
        {
            IsBusy = true;
            isSuccessful = await loginCheckService.CheckLogin(Login);
            if (isSuccessful)
            {
                IsLoginWrong = false;
            }
            else
            {
                ErrorMessage = "Same login already exist!";
                IsLoginWrong = true;
            }
            IsBusy = false;
        }
    }
}
