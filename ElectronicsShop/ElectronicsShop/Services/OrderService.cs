namespace ElectronicsShop.Services
{
    public class OrderService
    {
        public async Task CheckoutAsync(string userName, List<CartProduct> products, string address, string email, double totalPrice)
        {
            var orders = await DataSourceService<Order>.GetDataAsync();
            
            int newId = orders.Count() == 0 ? 1 : (from order in orders select order.OrderId).Max() + 1;
            Order newOrder = new()
            {
                Products = products, 
                OrderTime = DateTime.Now,
                TotalPrice = totalPrice, 
                OrderId = newId, 
                Address = address, 
                Email = email,
                Status = OrderStatus.AWAITING_CONFIRMATION, 
                UserName = userName,
            };

            await DataSourceService<Order>.SaveDataAsync(newOrder);
        }
        public async Task<List<Order>> GetOrdersAsync(string userName)
        {
            var orders = await DataSourceService<Order>.GetMultipleDataAsync(userName, nameof(Order.UserName));

            return orders.Any() ? orders.ToList<Order>() : new List<Order>();
        }
        public async Task<Order> ChangeStatusAsync(int orderId, string status)
        {

            var order = await DataSourceService<Order>.GetDataAsync(orderId, nameof(Order.OrderId));
            if (order is null) return null;
            order.Status = status;

            await DataSourceService<Order>.AlterSingleElementAsync(order, orderId, nameof(Order.OrderId));

            return order;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await DataSourceService<Order>.GetDataAsync();

            return orders.Any() ? orders.ToList<Order>() : new List<Order>();
        }
    }
}
