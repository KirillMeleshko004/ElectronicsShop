using System.Collections.Specialized;
using System.Linq;
using System.Text.Json;

namespace ElectronicsShop.Models
{
    public class Cart
    {
        static string _path = FileSystem.Current.AppDataDirectory;
        static string _fullPath = Path.Combine(_path, "CartData.json");

        static List<Product> _products;
        static List<Product> ReadProducts()
        {
            List<Product> products;
            using (FileStream fs = new(_fullPath, FileMode.OpenOrCreate))
            {
                try
                {
                    products = JsonSerializer.Deserialize<List<Product>>(fs);
                }
                catch (Exception)
                {
                    products = new List<Product>();
                }
            }
            return products;
        }
        static async Task WriteProducts(List<Product> products)
        {
            using (FileStream fs = new(_fullPath, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<List<Product>>(fs, products);
            }
        }
        static Cart()
        {
            _products = ReadProducts();
        }
        public static List<Product> GetCartList()
        {
            return _products;
        }
        public async static Task<List<Product>> AddProduct(Product product)
        {
            _products.Add(product);
            await WriteProducts(_products);
            return _products;
        }
        public async static Task<List<Product>> RemoveProduct(Product product)
        {
            Product tempProd = _products.Find(pr => pr.Id == product.Id);
            if (tempProd.Quantity != 1) tempProd.Quantity--;
            else _products.Remove(tempProd);
            await WriteProducts(_products);
            return _products;
        }
    }
}
