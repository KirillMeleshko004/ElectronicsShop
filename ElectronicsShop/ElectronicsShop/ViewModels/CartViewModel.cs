using ElectronicsShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        CartService _cartService;

        [ObservableProperty]
        ObservableCollection<Product> products;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            _cartService.CartChanged += UpdateCart;

            Products = new(cartService.GetCartList());
        }
        void UpdateCart()
        {
            var newProducts = from pr in _cartService.GetCartList() where !Products.Contains(pr) select pr;
            foreach (Product product in newProducts)
            {
                Products.Add(product);
            }
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
    }
}
