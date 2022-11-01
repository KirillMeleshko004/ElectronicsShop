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
        [NotifyPropertyChangedFor(nameof(IsNotSuccessful))]
        bool isSuccessful;

        public bool IsNotSuccessful => !isSuccessful;

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
            if (accountInfo.Error == AccountInfo.Errors.NoErrors) IsSuccessful = true;
            if (IsSuccessful)
            {
            }
            else
            {
                ErrorMessage = accountInfo.ErrorMessage;
            }
            IsBusy = false;
        }

        [RelayCommand]
        Task GoBack()
        {
            return Shell.Current.GoToAsync("..");
        }
    }
}
