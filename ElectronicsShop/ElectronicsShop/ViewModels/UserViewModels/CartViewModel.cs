using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        readonly CartService _cartService;

        [ObservableProperty]
        ObservableCollection<CartProduct> products;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            _cartService.CartChanged += UpdateCart;
            PropertyChanged += CollectionChanged;

            GetCart();
        }
        async void GetCart()
        {
            Products = (await _cartService.GetCartForUserAsync(App.UserName))?
                .ToObservableCollection<CartProduct>();
        }
        void UpdateCart(object sender, CartEventArgs e)
        {
            if(e.Product is null)
            {
                Products.Clear();
                return;
            }
            List<CartProduct> products = 
                (from product in Products where product.Equals(e.Product) select product).ToList();
            if (!Products.Contains(e.Product)) 
                Products.Add(e.Product);
            else
            {
                if(Products[Products.IndexOf(e.Product)].Quantity == 0)
                {
                    Products.Remove(e.Product);
                }
                else
                    Products[Products.IndexOf(e.Product)] = e.Product;
                IsEmpty = Products.Count == 0;
            }
        }

        [RelayCommand]
        async void RemoveProduct(CartProduct product)
        {
            IsBusy = true;
            await _cartService.RemoveProductFromCartAsync(App.UserName, product);
            IsBusy = false;
        }

        [RelayCommand]
        async Task GoToProduct(CartProduct currentProduct)
        {
            //Костыль!!!!!!!!!!
            Product prod = new Product 
            {
                Id = currentProduct.Id,
                ProductName = currentProduct.ProductName,
                ProductCategory = currentProduct.ProductCategory,
                Manufacturer = currentProduct.Manufacturer,
                Price = currentProduct.Price,
                Description = currentProduct.Description,
                ImageString = currentProduct.ImageString,
             };
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = prod
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
                    ["UserName"] = App.UserName,
                    ["TotalPrice"] = totalPrice
                });
        }
        void CollectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Products)) return;

            IsEmpty = Products.Count == 0;
        }
        public void Refresh()
        {
            PropertyChanged += CollectionChanged;

            GetCart();
        }
    }
}
