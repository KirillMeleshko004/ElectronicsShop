using CommunityToolkit.Maui.Core.Extensions;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(IsSignedIn), nameof(IsSignedIn))]
    public partial class CartViewModel : BaseViewModel
    {
        readonly CartService _cartService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotSignedIn))]
        bool isSignedIn;
        public bool IsNotSignedIn => !isSignedIn;

        [ObservableProperty]
        ObservableCollection<Product> products;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            _cartService.CartChanged += UpdateCart;
            PropertyChanged += CollectionChanged;

            IsSignedIn = App.UserAccount.IsSignedIn;
            App.UserAccount.PropertyChanged += AccountStateUpdated;

            Products = new(_cartService.GetCartList());
        }
        void UpdateCart()
        {
            Products = _cartService.GetCartList().ToObservableCollection<Product>();
        }

        [RelayCommand]
        void RemoveProduct(Product product)
        {
            Products = new(_cartService.RemoveProduct(product));
        }

        [RelayCommand]
        async Task AddProduct(Product product)
        {
            Products = new(await _cartService.AddProduct(product));
        }

        [RelayCommand]
        async Task GoToProduct(Product currentProduct)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct
                });
        }
        [RelayCommand]
        async Task Checkout()
        {
            if (!Products.Any()) return;
            double totalPrice = 0;
            foreach(Product product in Products)
            {
                totalPrice += product.Price * product.Quantity;
            }
            await Shell.Current.GoToAsync($"{nameof(CheckoutView)}",
                new Dictionary<string, object>
                {
                    ["Products"] = Products,
                    ["UserName"] = App.UserAccount.Login,
                    ["TotalPrice"] = totalPrice
                });
        }

        [RelayCommand]
        async void SignIn()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(AuthorizationView)}");
            IsBusy = false;
        }

        void AccountStateUpdated(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Account.IsSignedIn)) return;
            this.IsSignedIn = !App.UserAccount.IsSignedIn;
            this.IsSignedIn = App.UserAccount.IsSignedIn;
        }
        void CollectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Products)) return;
            if (Products.Count == 0) IsEmpty = true;
            else IsEmpty = false;
        }
        public void Refresh()
        {
            _cartService.CartChanged += UpdateCart;
            PropertyChanged += CollectionChanged;

            IsSignedIn = App.UserAccount.IsSignedIn;
            App.UserAccount.PropertyChanged += AccountStateUpdated;

            Products = new(_cartService.GetCartList());
        }
    }
}
