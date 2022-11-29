using System.ComponentModel;

namespace ElectronicsShop.Models
{
    public partial class Account : ObservableObject
    {
        [ObservableProperty]
        bool _isSignedIn;

        public List<Product> Favourites { get; set; }
        public string UserName { get; set; }

        readonly FavouritesService _favouritesService;
        public Account() 
        {
            IsSignedIn = false;
            UserName = null;
            _favouritesService = new();
            PropertyChanged += SignedIn;
        }

        async void SignedIn(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IsSignedIn)) return;
            if (IsSignedIn) Favourites = await _favouritesService.GetFavouritesForUserAsync(UserName);
            else Favourites = null;
        }
    }
}
