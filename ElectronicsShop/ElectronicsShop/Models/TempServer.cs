using System.Text.Json;
using static ElectronicsShop.GlobalConst.SearchConst;

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
        static readonly Dictionary<string, List<Product>> _favourites;
        static readonly List<Product> _products;

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

        static List<Order> ReadOrders()
        {
            List<Order> orders;
            using (FileStream fs = new(fullOrderPath, FileMode.OpenOrCreate))
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
            foreach (Order order in orders)
            {
                foreach (Product product in order.Products)
                    product.ProductImage = ImageSource.FromFile(product.ImageString);
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

        static Dictionary<string, List<Product>> ReadFavourites()
        {
            Dictionary<string, List<Product>> products;
            using (FileStream fs = new(fullOrderPath, FileMode.OpenOrCreate))
            {
                try
                {
                    products = JsonSerializer.Deserialize<Dictionary<string, List<Product>>>(fs);
                }
                catch (Exception)
                {
                    products = new Dictionary<string, List<Product>>();
                }
            }
            foreach (KeyValuePair<string, List<Product>> kvp in products)
            {
                foreach(Product product in kvp.Value)
                    product.ProductImage = ImageSource.FromFile(product.ImageString);
            }
            return products;
        }
        static void WriteFavourites(Dictionary<string, List<Product>> favourites)
        {
            using (FileStream fs = new(fullOrderPath, FileMode.Create))
            {
                JsonSerializer.Serialize<Dictionary<string, List<Product>>>(fs, favourites);
            }
        }
        static TempServer()
        {
            _accounts = ReadAccounts();

            _orders = ReadOrders();

            _favourites = ReadFavourites();

            _products = new()
            {
                new Product(KITCHEN_CATEGORY, 1, "Fridge_1", "lol_1_manufacturer", 10000, "lol_1_description", 0, "fridge.jpg"),
                new Product(KITCHEN_CATEGORY, 2, "Toaster_1", "lol_2_manufacturer", 10000, "lol_2_description", 0, "toaster.jpg"),
                new Product(TV_CATEGORY, 3, "TV_1", "lol_3_manufacturer", 10000, "lol_3_description", 0, "tv.jpg"),
                new Product(TV_CATEGORY, 4, "TV_2", "lol_3_manufacturer", 10000, "lol_3_description", 0, "tv2.jpg"),
                new Product(SMARTPHONES_CATEGORY, 5, "Smartphone_1", "lol_3_manufacturer", 10000, "lol_3_description", 0, "smartphone.jpg"),
                new Product(HOME_CATEGORY, 6, "Homeapl_1", "lol_3_manufacturer", 10000, "lol_3_description", 0, "washing_machine.jpg"),
                new Product(LAPTOPS_CATEGORY, 7, "Laptop_1", "lol_3_manufacturer", 10000, "lol_3_description", 0, "laptop.jpg")
            };
        }
        public async static Task<AccountInfo> AuthorizationAsync(string login, string password)
        {
            await Task.Delay(1);
            if (login == null || password == null || login == "" || password == "") return new AccountInfo(login, 
                AccountErrorMessages.FIELDS_EMPTY);
            if (!_accounts.ContainsKey(login) || _accounts[login] != password) return new AccountInfo(login,
                AccountErrorMessages.INCORRECT_LOGIN_OR_PASSWORD);
            return new AccountInfo(login, AccountErrorMessages.SUCCESS);
        }
        public async static Task<AccountInfo> CreateAccountAsync(string login, string password, string repeatPassword)
        {
            await Task.Delay(1);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword)) 
                return new AccountInfo(login, AccountErrorMessages.FIELDS_EMPTY);
            if (_accounts.ContainsKey(login)) return new AccountInfo(login, AccountErrorMessages.SAME_LOGIN_EXIST);
            if (password != repeatPassword) return new AccountInfo(login, AccountErrorMessages.PASSWORDS_NOT_SAME);
            _accounts.Add(login, password);
            WriteAccounts(_accounts);
            return new AccountInfo(login, AccountErrorMessages.SUCCESS);
        }
        public async static Task<AccountInfo> ChangePasswordAsync(string login, string oldPassword, string newPassword, string repeatPassword)
        {
            await Task.Delay(1);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(oldPassword) || 
                string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(repeatPassword))
                return new AccountInfo(login, AccountErrorMessages.FIELDS_EMPTY);
            if (_accounts[login] != oldPassword) return new AccountInfo(login,
                AccountErrorMessages.INCORRECT_OLD_PASSWORD);
            else if (newPassword!=repeatPassword) return new AccountInfo(login, AccountErrorMessages.PASSWORDS_NOT_SAME);
            _accounts[login] = newPassword;
            WriteAccounts(_accounts);
            return new AccountInfo(login, AccountErrorMessages.SUCCESS);
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

        public async static Task<List<Product>> GetFavouritesForUserAsync(string userName)
        {
            await Task.Delay(10);
            if (_favourites.ContainsKey(userName))
                return _favourites[userName];
            else return new List<Product>();
        }
        public async static Task SetFavouriteAsync(string userName, int productId)
        {
            await Task.Delay(10);
            if (_favourites.ContainsKey(userName))
                _favourites[userName].AddRange(from product in _products where product.Id == productId select product);
            else
            {
                List<Product> newUserFav = new List<Product>(from product in _products where product.Id == productId select product);
                _favourites.Add(userName, newUserFav);
            }
            WriteFavourites(_favourites);
        }
        public async static Task<bool> IsProductFavouriteForUserAsync(string userName, int productId)
        {
            await Task.Delay(10);
            if (!_favourites.ContainsKey(userName)) return false;
            return (from product in _favourites[userName] where product.Id == productId select product).Any();
        }
        public async static Task<List<Product>> GetProductsAsync()
        {
            await Task.Delay(1);
            return _products;
        }
        public static List<Product> GetProducts()
        {
            return _products;
        }
    }
}
