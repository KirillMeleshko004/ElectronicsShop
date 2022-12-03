using System.ComponentModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    [QueryProperty(nameof(CurrentProduct), nameof(CurrentProduct))]
    public partial class ProductDetailsViewModel : BaseViewModel
    {
        readonly CartService _cartService;
        readonly FavouritesService _favouritesService;

        [ObservableProperty]
        Product _currentProduct;
        [ObservableProperty]
        int _countInCart;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotFavouriteForUser))]
        bool _isFavouriteForUser = false;
        public bool IsNotFavouriteForUser => !_isFavouriteForUser;

        [ObservableProperty]
        bool _isSignedIn;

        public ProductDetailsViewModel(CartService cartService, FavouritesService favouritesService)
        {
            _cartService = cartService;
            _favouritesService = favouritesService;

            IsSignedIn = App.UserAccount.IsSignedIn;

            PropertyChanged += ProductPropertyChanged;
        }
        public async void ProductPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CurrentProduct)) return;
            CountInCart = await _cartService.CountProductInCart(CurrentProduct.Id);
            if (IsSignedIn) IsFavouriteForUser = await _favouritesService.IsProductFavouriteForUserAsync(App.UserAccount.UserName, CurrentProduct.Id);
        }

        [RelayCommand]
        async Task AddToCart(Product product)
        {
            List<Product> newCartList = await _cartService.AddProduct(product);
            CountInCart = (from pr in newCartList where pr.Id == CurrentProduct.Id select pr.Quantity).First<int>();
        }

        [RelayCommand]
        async Task AddToFavourites()
        {
            if (IsBusy) return;
            IsBusy = true;
            if(IsFavouriteForUser)
                await _favouritesService.DeleteFromFavouritesAsync(App.UserAccount.UserName, CurrentProduct.Id);
            else await _favouritesService.SetFavouriteAsync(App.UserAccount.UserName, CurrentProduct.Id);
            IsFavouriteForUser = !IsFavouriteForUser;
            IsBusy = false;
        }
    }
}
