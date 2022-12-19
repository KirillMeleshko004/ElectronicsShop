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
            ImageURI = product.ImageURI;
            Quantity = 0;
        }

        public Product GetProduct()
        {
            return new Product
            {
                Id = Id,
                ProductName = ProductName,
                ProductCategory = ProductCategory,
                Manufacturer = Manufacturer,
                Price = Price,
                Description = Description,
                ImageURI = ImageURI,
            };
        }
    }
}