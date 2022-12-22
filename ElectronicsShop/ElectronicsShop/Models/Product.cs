namespace ElectronicsShop.Models
{
    public class Product : IImageURIContainer
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageURI { get; set; }

        public Product CloneProductWithCategory(string newCategory)
        {
            return new Product
            {
                Id = Id,
                ProductName = ProductName,
                ProductCategory = newCategory,
                Manufacturer = Manufacturer,
                Price = Price,
                Description = Description,
                ImageURI = ImageURI
            };
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            
            Product pr = (Product)obj;
            return pr.Id == this.Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
