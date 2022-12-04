namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class AdminShellViewModel : ObservableObject
    {
        [ObservableProperty]
        string userName;

        public AdminShellViewModel()
        {
            UserName = App.UserName;
        }

        [RelayCommand]
        void SignOut()
        {
            App.Current.MainPage = new AuthShell();
        }
    }
}
