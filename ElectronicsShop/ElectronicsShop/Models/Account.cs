namespace ElectronicsShop.Models
{
    public partial class Account : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool isSignedIn;

        public bool IsNotSignedIn => !isSignedIn;
        public string Login { get; set; }

        public Account(string login)
        {
            Login = login;
        }
        public Account() { }
    }
}
