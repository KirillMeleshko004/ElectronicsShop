namespace ElectronicsShop.Models
{
    public partial class Account : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool isSignedIn;

        public bool IsNotSignedIn => !isSignedIn;
        string _login;
        public string Login { 
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                LoginChanged?.Invoke(this, new EventArgs());
            }
        }

        public Account(string login)
        {
            Login = login;
        }
        public Account() { }

        public event EventHandler LoginChanged;
    }
}
