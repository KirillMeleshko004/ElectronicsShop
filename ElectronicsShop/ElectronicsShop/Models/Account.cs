using System.ComponentModel;

namespace ElectronicsShop.Models
{
    public partial class Account : ObservableObject
    {
        [ObservableProperty]
        bool _isSignedIn;

        public string UserName { get; set; }
        public string Role { get; set; }

        public Account() 
        {
            IsSignedIn = false;
            PropertyChanged += SignedIn;
        }

        void SignedIn(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IsSignedIn)) return;
        }
    }
}
