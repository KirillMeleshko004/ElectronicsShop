using System.ComponentModel;

namespace ElectronicsShop.Models
{
    public partial class Account : INotifyPropertyChanged
    {
        bool _isSignedIn;
        public bool IsSignedIn
        {
            get
            {
                return _isSignedIn;
            }
            set
            {
                _isSignedIn = value;
                OnPropertyChanged(nameof(IsSignedIn));
                OnPropertyChanged(nameof(IsNotSignedIn));
                AccountStateChanged?.Invoke(this, new EventArgs());
            }
        }

        public bool IsNotSignedIn => !_isSignedIn;

        string _login;
        public string Login { 
            get
            {
                return _login;
            }
            set
            {
                _login = value;
            }
        }
        public Account() 
        {
            IsSignedIn = false;
            Login = null;
        }

        public event EventHandler AccountStateChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
