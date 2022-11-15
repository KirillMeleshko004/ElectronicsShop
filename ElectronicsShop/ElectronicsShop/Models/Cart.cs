using System.Collections.Specialized;
using System.Linq;
using System.Text.Json;

namespace ElectronicsShop.Models
{
    public class Cart
    {
        static string _path = FileSystem.Current.CacheDirectory;
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
            foreach (Product pr in products)
            {
                pr.ProductImage = ImageSource.FromFile(pr.ImageString);
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
            if (!_products.Contains(product))
            {
                product.Quantity++;
                _products.Add(product);
            }
            else
            {
                _products[_products.IndexOf(product)].Quantity++;
            }
            await WriteProducts(_products);
            return _products;
        }
        public async static Task<List<Product>> RemoveProduct(Product product)
        {
            Product tempProd = _products.Find(pr => pr.Id == product.Id);
            if (tempProd.Quantity != 1) tempProd.Quantity--;
            else _products.Remove(tempProd);
            await Task.Run(()=>WriteProducts(_products));
            return _products;
        }
    }
}
