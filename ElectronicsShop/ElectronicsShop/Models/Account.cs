using System.ComponentModel;

namespace ElectronicsShop.Models
{
    public partial class Account : ObservableObject
    {
        [ObservableProperty]
        bool _isSignedIn;

        public string Login { get; set; }
        public Account() 
        {
            IsSignedIn = false;
            Login = null;
        }
    }
}
