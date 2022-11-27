using System.ComponentModel;

namespace ElectronicsShop.Models
{
    public partial class Account : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool _isSignedIn;

        public bool IsNotSignedIn => !_isSignedIn;
        public string Login { get; set; }
        public Account() 
        {
            IsSignedIn = false;
            Login = null;
        }
    }
}
