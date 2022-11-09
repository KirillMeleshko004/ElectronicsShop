namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(CurrentProduct), nameof(CurrentProduct))]
    public partial class ProductViewModel : BaseViewModel
    {
        CartService _cartService;

        [ObservableProperty]
        Product currentProduct;

        public ProductViewModel(CartService cartService)
        {
            _cartService = cartService;
        }

        [RelayCommand]
        async Task AddToCart(Product product)
        {
            await _cartService.AddProduct(product);
        }
    }
}
