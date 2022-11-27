namespace ElectronicsShop.Services
{
    public class OrderService
    {
        public async Task<bool> СheckoutAsync(List<Product> products, string userName, 
            DateTime orderTime, double totalPrice)
        {
            return await TempServer.Checkout(products, userName, orderTime, totalPrice);
        }
        public async Task<List<Order>> GetOrdersAsync(string userName)
        {
            return await TempServer.GetOrders(userName);
        }
    }
}
