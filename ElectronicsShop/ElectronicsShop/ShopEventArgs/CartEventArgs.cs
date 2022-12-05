namespace ElectronicsShop.ShopEventArgs
{
    public class CartEventArgs : EventArgs
    {
        public CartProduct Product { get; set; }
        public CartEventArgs(CartProduct product)
        {
            Product = product;
        }
    }
}
