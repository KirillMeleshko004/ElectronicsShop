using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(CurrentProduct), nameof(CurrentProduct))]
    public partial class ProductDetailsViewModel : BaseViewModel
    {
        readonly CartService _cartService;
        readonly FavouritesService _favouritesService;

        [ObservableProperty]
        Product currentProduct;
        [ObservableProperty]
        int countInCart;
        [ObservableProperty]
        bool isFavouriteForUser;

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
        }

        [RelayCommand]
        async Task AddToCart(Product product)
        {
            List<Product> newCartList = await _cartService.AddProduct(product);
            CountInCart = (from pr in newCartList where pr.Id == CurrentProduct.Id select pr.Quantity).First<int>();
        }
    }
}
