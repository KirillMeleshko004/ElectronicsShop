using ElectronicsShop.Models;
using System.Text.Json;

namespace ElectronicsShop.Services
{
    public class CartService
    {

        public List<Product> GetCartList()
        {
            return Cart.GetCartList();
        }
        public async Task<List<Product>> AddProduct(Product product)
        {
            List<Product> tempList = await Cart.AddProduct(product);
            if(CartChanged != null) CartChanged.Invoke();
            return tempList;
        }
        public List<Product> RemoveProduct(Product product)
        {
            List<Product> tempList = Cart.RemoveProduct(product);
            CartChanged.Invoke();
            return tempList;
        }

        public event CartUpdated CartChanged;

        public delegate void CartUpdated();
    }
}
