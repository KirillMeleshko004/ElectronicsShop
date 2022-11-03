namespace ElectronicsShop.Models
{
    public class Product
    {
        public string ProductType { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public ImageSource ProductImage { get; set; }
        public Product(string productType, int id, string productName, string manufacturer, double price, string description, int quantity, string image)
        {
            ProductType = productType;
            Id = id;
            ProductName = productName;
            Manufacturer = manufacturer;
            Price = price;
            Description = description;
            Quantity = quantity;
            ProductImage = ImageSource.FromFile(image);
        }
    }
}
