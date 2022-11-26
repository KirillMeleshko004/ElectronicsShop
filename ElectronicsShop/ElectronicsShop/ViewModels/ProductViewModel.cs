using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(CurrentProduct), nameof(CurrentProduct))]
    public partial class ProductViewModel : BaseViewModel, INotifyPropertyChanged
    {
        CartService _cartService;

        Product currentProduct;
        int countInCart;

        public Product CurrentProduct
        {
            get 
            { 
                return currentProduct; 
            }
            set 
            {
                currentProduct = value;
                OnPropertyChanged(nameof(CurrentProduct));
                Product currentProductInCart = (from pr in _cartService.GetCartList() where pr.Id == currentProduct.Id select pr)
                    .FirstOrDefault<Product>((Product)null);
                if (currentProductInCart is not null) CountInCart = currentProductInCart.Quantity;
                else CountInCart = 0;
            }
        }

        public int CountInCart
        {
            get { return countInCart; }
            set
            {
                countInCart = value;
                OnPropertyChanged(nameof(CountInCart));
            }
        }

        public ProductViewModel(CartService cartService)
        {
            _cartService = cartService;
        }

        [RelayCommand]
        async Task AddToCart(Product product)
        {
            List<Product> newCartList = await _cartService.AddProduct(product);
            CountInCart = (from pr in newCartList where pr.Id == CurrentProduct.Id select pr.Quantity).First<int>();
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        public new void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
