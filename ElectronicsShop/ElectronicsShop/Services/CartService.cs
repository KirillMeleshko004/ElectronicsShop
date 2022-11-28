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
            CartChanged?.Invoke();
            return tempList;
        }
        public List<Product> RemoveProduct(Product product)
        {
            List<Product> tempList = Cart.RemoveProduct(product);
            CartChanged.Invoke();
            return tempList;
        }
        public void ClearCart()
        {
            Cart.ClearCart();
            CartChanged?.Invoke();
        }

        public event CartUpdated CartChanged;

        public delegate void CartUpdated();
    }
}
