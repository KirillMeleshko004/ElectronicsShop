namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [ObservableProperty]
        string userName;

        public AppShellViewModel()
        {
            UserName = App.UserName;
        }

        [RelayCommand]
        void SignOut()
        {
            UserName = null;
            App.UserName = null;
            App.Current.MainPage = new AuthShell();
        }
    }
}
