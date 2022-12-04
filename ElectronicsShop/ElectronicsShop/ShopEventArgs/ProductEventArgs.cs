namespace ElectronicsShop.ShopEventArgs
{
    public class ProductEventArgs : EventArgs
    {
        public Product Product { get; set; }
        public Actions Action { get; set; }

        public ProductEventArgs(Product product, Actions action)
        {
            Product = product;
            Action = action;
        }

        public enum Actions
        {
            added,
            removed,
            changed
        }
    }
}
