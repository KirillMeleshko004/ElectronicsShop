using System.Text.Json;
using static ElectronicsShop.GlobalConst.CategoriesConst;

namespace ElectronicsShop.Models
{
    public class TempServer
    {
        static readonly string orderPath = FileSystem.Current.AppDataDirectory;
        static readonly string fullOrderPath = Path.Combine(orderPath, "OrderData.json");

        static readonly Dictionary<string, List<Product>> _favourites;
        static readonly List<Product> _products;

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


            _favourites = ReadFavourites();
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
        public async static Task DeleteFromFavouritesAsync(string userName, int productId)
        {
            await Task.Delay(10);
            if (_favourites.ContainsKey(userName))
                _favourites[userName].Remove((from product in _products where product.Id == productId select product).ElementAt(0));
            WriteFavourites(_favourites);
        }
        public async static Task<bool> IsProductFavouriteForUserAsync(string userName, int productId)
        {
            await Task.Delay(10);
            if (!_favourites.ContainsKey(userName)) return false;
            return (from product in _favourites[userName] where product.Id == productId select product).Any();
        }
    }
}
