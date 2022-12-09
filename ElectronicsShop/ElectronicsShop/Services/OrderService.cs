using Firebase.Database;
using Firebase.Database.Query;

namespace ElectronicsShop.Services
{
    public class OrderService
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);

        public async Task CheckoutAsync(string userName, List<CartProduct> products, Address address, double totalPrice)
        {
            int newId = (await _firebaseClient.Child(nameof(Order)).OnceAsync<Order>())?.Count + 1 ?? 1;
            Order newOrder = new Order(products, DateTime.Now, totalPrice, newId, address, OrderStatus.AWAITING_CONFIRMATION, userName);

            await _firebaseClient.Child(nameof(Order)).PostAsync(newOrder);
        }
        public async Task<List<Order>> GetOrdersAsync(string userName)
        {       
            var orders = (await _firebaseClient
                .Child(nameof(Order))
                .OrderBy(nameof(Order.UserName))
                .EqualTo(userName)
                .OnceAsync<Order>());
            return orders is null ? new List<Order>() : (from obj in orders select obj.Object).ToList<Order>();
        }
        public async Task<Order> ChangeStatusAsync(int orderId, string status)
        {
            var order = (await _firebaseClient
                .Child(nameof(Order))
                .OrderBy(nameof(Order.OrderId))
                .EqualTo(orderId)
                .OnceAsync<Order>())
                .FirstOrDefault((FirebaseObject<Order>)null);
            if (order is null) return null;
            order.Object.Status = status;
            await _firebaseClient
                .Child(nameof(Order))
                .Child(order.Key)
                .PatchAsync(order.Object);
            return order.Object;
        }
    }
}
