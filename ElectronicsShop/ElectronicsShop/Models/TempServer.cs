using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ElectronicsShop.Models
{
    public class TempServer
    {
        static readonly string path = FileSystem.Current.AppDataDirectory;
        static readonly string fullPath = Path.Combine(path, "UserData.json");
        static readonly string orderPath = FileSystem.Current.AppDataDirectory;
        static readonly string fullOrderPath = Path.Combine(orderPath, "OrderData.json");

        static readonly Dictionary<string, string> _accounts;
        static readonly List<Order> _orders;

        static Dictionary<string, string> ReadAccounts()
        {
            Dictionary<string, string> accounts;
            using (FileStream fs = new(fullPath, FileMode.OpenOrCreate))
            {
                try
                {
                    accounts = JsonSerializer.Deserialize<Dictionary<string, string>>(fs);
                }
                catch (Exception)
                {
                    accounts = new Dictionary<string, string>();
                }
            }
            return accounts;
        }
        static void WriteAccounts(Dictionary<string, string> accounts)
        {
            using (FileStream fs = new(fullPath, FileMode.Create))
            {
                JsonSerializer.Serialize<Dictionary<string, string>>(fs, accounts);
            }
        }
        static TempServer()
        {
            _accounts = ReadAccounts();

            _orders = ReadOrders();
        }
        public async static Task<AccountInfo> AuthorizationAsync(string login, string password)
        {
            await Task.Delay(500);
            if (login == null || password == null || login == "" || password == "") return new AccountInfo(login, 
                AccountErrorMessages.FIELDS_EMPTY);
            if (!_accounts.ContainsKey(login) || _accounts[login] != password) return new AccountInfo(login,
                AccountErrorMessages.INCORRECT_LOGIN_OR_PASSWORD);
            return new AccountInfo(login, AccountErrorMessages.SUCCESS);
        }
        public async static Task<AccountInfo> CreateAccountAsync(string login, string password, string repeatPassword)
        {
            await Task.Delay(3000);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword)) 
                return new AccountInfo(login, AccountErrorMessages.FIELDS_EMPTY);
            if (_accounts.ContainsKey(login)) return new AccountInfo(login, AccountErrorMessages.SAME_LOGIN_EXIST);
            if (password != repeatPassword) return new AccountInfo(login, AccountErrorMessages.PASSWORDS_NOT_SAME);
            _accounts.Add(login, password);
            WriteAccounts(_accounts);
            return new AccountInfo(login, AccountErrorMessages.SUCCESS);
        }

        public static List<Product> FilterProducts(Product filterProduct)
        {
            List<Product> products = new ProductsService().GetProducts();
            return (from pr in products where pr.ProductCategory == filterProduct.ProductCategory select pr).ToList<Product>();
        }


        public async static Task<bool> Checkout(Order order)
        {
            _orders.Add(order);
            await Task.Delay(100);
            WriteOrders(_orders);
            return true;
        }
        public async static Task<int> GetNewOrderId()
        {
            var ids = (from or in _orders select or.OrderId);
            int newId;
            if (ids.Any())
                newId = ids.Max<int>() + 1;
            else newId = 1; 
            await Task.Delay(100);
            return newId;
        }

        public async static Task<List<Order>> GetOrders(string userName)
        {
            await Task.Delay(100);
            return (from order in _orders where order.UserName == userName select order).ToList<Order>();
        }

        static List<Order> ReadOrders()
        {
            List<Order> orders;
            using (FileStream fs = new(fullOrderPath, FileMode.Create))
            {
                try
                {
                    orders = JsonSerializer.Deserialize<List<Order>>(fs);
                }
                catch (Exception)
                {
                    orders = new List<Order>();
                }
            }
            return orders;
        }
        static void WriteOrders(List<Order> orders)
        {
            using (FileStream fs = new(fullOrderPath, FileMode.Create))
            {
                JsonSerializer.Serialize<List<Order>>(fs, orders);
            }
        }
    }
}
