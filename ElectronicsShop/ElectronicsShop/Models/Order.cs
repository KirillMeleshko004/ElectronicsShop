namespace ElectronicsShop.Models
{
    public class Order
    {
        public List<Product> Products { get; private set; }
        public DateTime OrderTime { get; private set; }
        public string UserName { get; private set; }

        public Order(List<Product> products, DateTime orderTime, string userName)
        {
            Products = products;
            OrderTime = orderTime;
            UserName = userName;
        }
    }
}
