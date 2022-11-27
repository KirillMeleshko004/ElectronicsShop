using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(CurrentProduct), nameof(CurrentProduct))]
    public partial class ProductViewModel : BaseViewModel
    {
        readonly CartService _cartService;

        [ObservableProperty]
        Product currentProduct;
        [ObservableProperty]
        int countInCart;

        public ProductViewModel(CartService cartService)
        {
            _cartService = cartService;

            PropertyChanged += ProductChanged;
        }
        public void ProductChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CurrentProduct)) return;
            Product currentProductInCart = (from pr in _cartService.GetCartList() where pr.Id == currentProduct.Id select pr)
                    .FirstOrDefault<Product>((Product)null);
            if (currentProductInCart is not null) CountInCart = currentProductInCart.Quantity;
            else CountInCart = 0;
        }

        [RelayCommand]
        async Task AddToCart(Product product)
        {
            List<Product> newCartList = await _cartService.AddProduct(product);
            CountInCart = (from pr in newCartList where pr.Id == CurrentProduct.Id select pr.Quantity).First<int>();
        }
    }
}
