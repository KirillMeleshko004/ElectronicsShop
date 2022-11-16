using CommunityToolkit.Maui.Core.Extensions;
using ElectronicsShop.Views;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ObjectiveC;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class CartViewModel : BaseViewModel
    {
        CartService _cartService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool isSignedIn;

        public bool IsNotSignedIn => !isSignedIn;

        [ObservableProperty]
        ObservableCollection<Product> products;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            _cartService.CartChanged += UpdateCart;
            IsSignedIn = App.UserAccount.IsSignedIn;
            App.UserAccount.LoginChanged += AccountStateUpdated;

            Products = new(cartService.GetCartList());
        }
        void UpdateCart()
        {
            Products = _cartService.GetCartList().ToObservableCollection<Product>();
        }

        [RelayCommand]
        async Task RemoveProduct(Product product)
        {
            Products = new(await _cartService.RemoveProduct(product));
        }

        [RelayCommand]
        async Task AddProduct(Product product)
        {
            Products = new(await _cartService.AddProduct(product));
        }

        [RelayCommand]
        async Task GoToProduct(Product currentProduct)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct
                });
        }

        [RelayCommand]
        async void SignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
        }

        void AccountStateUpdated(object sender, EventArgs e)
        {
            IsSignedIn = App.UserAccount.IsSignedIn;
        }
    }
}
