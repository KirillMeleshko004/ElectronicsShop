using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class CartViewModel : BaseViewModel, IRefreshableAsync
    {
        readonly CartService _cartService;

        [ObservableProperty]
        ObservableCollection<CartProduct> _products;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            PropertyChanged += CollectionChanged;

            RefreshAsync();
        }
        async void GetCart()
        {
            Products = (await _cartService.GetCartForUserAsync(App.UserName))?
                .ToObservableCollection<CartProduct>();
        }

        [RelayCommand]
        async Task RemoveProduct(CartProduct product)
        {
            IsBusy = true;
            CartProduct remProd = await _cartService.RemoveProductFromCartAsync(App.UserName, product);
            if (remProd.Quantity == 0) Products.Remove(product);
            else Products[Products.IndexOf(product)] = remProd;
            IsEmpty = !Products.Any();
            IsBusy = false;
        }
        [RelayCommand]
        async Task FullyRemoveProduct(CartProduct product)
        {
            IsBusy = true;
            await _cartService.FullRemoveProductAsync(App.UserName, product);
            Products.Remove(product);
            IsEmpty = !Products.Any();
            IsBusy = false;
        }

        [RelayCommand]
        async Task AddProduct(CartProduct product)
        {
            IsBusy = true;
            CartProduct newProd = await _cartService.AddProductToCartAsync(App.UserName, product);
            Products[Products.IndexOf(product)] = newProd;
            IsBusy = false;
        }

        [RelayCommand]
        async Task GoToProduct(CartProduct currentProduct)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct.GetProduct()
                });
        }
        [RelayCommand]
        async Task Checkout()
        {
            if (!Products.Any()) return;
            double totalPrice = 0;
            foreach (CartProduct product in Products)
            {
                totalPrice += product.Price * product.Quantity;
            }
            await Shell.Current.GoToAsync($"{nameof(CheckoutView)}",
                new Dictionary<string, object>
                {
                    ["Products"] = Products,
                    ["TotalPrice"] = totalPrice
                });
        }
        void CollectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Products)) return;

            IsEmpty = Products is null ? true : Products.Count == 0;
        }
        public void RefreshAsync()
        {
            GetCart();
        }
    }
}
