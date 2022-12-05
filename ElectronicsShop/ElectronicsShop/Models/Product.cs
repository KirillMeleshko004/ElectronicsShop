namespace ElectronicsShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageString { get; set; }
        public Product() { }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Product pr = (Product)obj;
                return pr.Id == this.Id;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
