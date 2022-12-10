namespace ElectronicsShop.Models
{
    public class Order
    {
        public List<CartProduct> Products { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalPrice { get; set; }
        public Address Address { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }

        public static bool IsCancelable(string status)
        {
            return status == OrderStatus.AWAITING_CONFIRMATION || status == OrderStatus.ACCEPTED;
        }
    }
}
