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

        public ProductDetailsViewModel(CartService cartService, FavouritesService favouritesService)
        {
            _cartService = cartService;
            _favouritesService = favouritesService;

            PropertyChanged += ProductPropertyChanged;
        }
        public async void ProductPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CurrentProduct)) return;
            CountInCart = await _cartService.CountProductInCart(CurrentProduct.Id);
            IsFavouriteForUser = await _favouritesService.IsProductFavouriteForUserAsync(App.UserName, CurrentProduct.Id);
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
            IsBusy = true;
            if(IsFavouriteForUser)
                await _favouritesService.DeleteFromFavouritesAsync(App.UserName, CurrentProduct.Id);
            else await _favouritesService.SetFavouriteAsync(App.UserName, CurrentProduct.Id);
            IsFavouriteForUser = !IsFavouriteForUser;
            IsBusy = false;
        }
    }
}
