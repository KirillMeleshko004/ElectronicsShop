namespace ElectronicsShop.Models
{
    public class Order
    {
        public List<Product> Products { get; private set; }
        public int OrderId { get; private set; }
        public DateTime OrderTime { get; private set; }
        public string UserName { get; private set; }
        public double TotalPrice { get; private set; }

        public Order(List<Product> products, DateTime orderTime, string userName, double totalPrice, int orderId)
        {
            Products = products;
            OrderTime = orderTime;
            UserName = userName;
            TotalPrice = totalPrice;
            OrderId = orderId;
        }
    }
}
