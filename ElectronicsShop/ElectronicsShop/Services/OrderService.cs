namespace ElectronicsShop.Services
{
    public class OrderService
    {
        public async Task CheckoutAsync(string userName, List<CartProduct> products, Address address, double totalPrice)
        {
            int newId = await DataSourceService<Order>.GetNewIdAsync();
            Order newOrder = new Order
            {
                Products = products, 
                OrderTime = DateTime.Now,
                TotalPrice = totalPrice, 
                OrderId = newId, 
                Address = address, 
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
        public async Task<bool> AddressValidation()
        {
            string address1 = "wfefew, wfewef, wefwfw, 12";
            IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address1);

            Location location = locations?.FirstOrDefault();

            if (location != null)
                Console.WriteLine($"Latitude: {location.Latitude}," +
                    $" Longitude: {location.Longitude}, Altitude: " +
                    $"{location.Altitude}");

            IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(55.493683, 28.750982);

            Placemark placemark = placemarks?.FirstOrDefault();
            return false;
        }
    }
}
