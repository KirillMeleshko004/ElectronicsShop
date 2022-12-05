namespace ElectronicsShop.Models
{
    public class CartProduct : Product
    {
        public int Quantity { get; set; }

        public CartProduct() : base()
        {
        }
        public CartProduct(Product product) : base()
        {
            Id = product.Id;
            ProductName = product.ProductName;
            ProductCategory = product.ProductCategory;
            Manufacturer = product.Manufacturer;
            Price = product.Price;
            Description = product.Description;
            ImageString = product.ImageString;
            Quantity = 0;
        }
    }
}