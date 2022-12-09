namespace ElectronicsShop.Models
{
    public class Order
    {
        public List<CartProduct> Products { get; private set; }
        public int OrderId { get; private set; }
        public DateTime OrderTime { get; private set; }
        public double TotalPrice { get; private set; }
        public Address Address { get; set; }
        public string Status { get; set; }
        public string UserName { get; private set; }

        public Order(List<CartProduct> products, DateTime orderTime, double totalPrice, int orderId, Address adress, string status, string userName)
        {
            Products = products;
            OrderTime = orderTime;
            TotalPrice = totalPrice;
            OrderId = orderId;
            Address = adress;
            Status = status;
            UserName = userName;
        }



        public static bool IsCancelable(string status)
        {
            return status == OrderStatus.AWAITING_CONFIRMATION || status == OrderStatus.ACCEPTED;
        }
    }
}
