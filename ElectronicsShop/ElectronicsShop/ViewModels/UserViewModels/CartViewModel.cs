using CommunityToolkit.Maui.Core.Extensions;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        readonly CartService _cartService;

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

            Products = new(_cartService.GetCartList());
        }
        void UpdateCart()
        {
            Products = _cartService.GetCartList().ToObservableCollection<Product>();
        }

        [RelayCommand]
        async void RemoveProduct(Product product)
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
                    ["UserName"] = App.UserName,
                    ["TotalPrice"] = totalPrice
                });
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

            Products = new(_cartService.GetCartList());
        }
    }
}
