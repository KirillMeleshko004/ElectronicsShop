using ElectronicsShop.Views;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class AppShellViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool _isSignedIn;
        public bool IsNotSignedIn => !_isSignedIn;


        //bool _isSignedIn;
        //public bool IsSignedIn
        //{
        //    get
        //    {
        //        return _isSignedIn;
        //    }
        //    set
        //    {
        //        _isSignedIn = value;
        //        OnPropertyChanged(nameof(IsSignedIn));
        //        OnPropertyChanged(nameof(IsNotSignedIn));
        //    }
        //}

        //public bool IsNotSignedIn => !_isSignedIn;

        public AppShellViewModel()
        {
            App.UserAccount.AccountStateChanged += AccountStateUpdated;
        }

        [RelayCommand]
        void SignOut()
        {
            IsSignedIn = false;
            App.UserAccount.IsSignedIn = false;
            App.UserAccount.Login = null;
        }


        [RelayCommand]
        async void SignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
        }

        void AccountStateUpdated(object sender, EventArgs e)
        {
            IsSignedIn = App.UserAccount.IsSignedIn;
        }

    }
}
