using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public partial class AdminShellViewModel : ObservableObject
    {
        [ObservableProperty]
        string userName;

        public AdminShellViewModel()
        {
            UserName = App.UserAccount.UserName;
        }

        [RelayCommand]
        void SignOut()
        {
            App.Current.MainPage = new AppShell();
        }
    }
}
