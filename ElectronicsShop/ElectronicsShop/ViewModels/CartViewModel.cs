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
        public List<Product> products;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            _cartService.CartChanged += UpdateCart;

            Products = cartService.GetCartList();
        }
        void UpdateCart()
        {
            Products = _cartService.GetCartList();
        }

        [RelayCommand]
        async Task RemoveProduct(Product product)
        {
            Products = await _cartService.RemoveProduct(product);
        }

        [RelayCommand]
        async Task AddProduct(Product product)
        {
            Products = await _cartService.AddProduct(product);
        }
    }
}
