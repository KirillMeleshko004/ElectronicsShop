namespace ElectronicsShop.Services
{
    public class OrderService
    {
        public async Task<bool> СheckoutAsync(Order order)
        {
            return await TempServer.Checkout(order);
        }
        public async Task<List<Order>> GetOrdersAsync(string userName)
        {
            return await TempServer.GetOrders(userName);
        }
    }
}
