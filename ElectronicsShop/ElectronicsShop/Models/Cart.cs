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
            return products;
        }
        static async Task WriteProducts(List<Product> products)
        {
            using (FileStream fs = new(_fullPath, FileMode.Create))
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
            await Task.Delay(1);
            Product tempProd = _products.Find(pr => pr.Id == product.Id);
            if (tempProd.Quantity != 1) tempProd.Quantity--;
            else _products.Remove(tempProd);
            await WriteProducts(_products);
            return _products;
        }
        public async static void ClearCart()
        {
            _products.Clear();
            await WriteProducts(_products);
        }
        public async static Task<int> CountProductInCart(int productId)
        {
            await Task.Delay(1);
            if (!_products.Any()) return 0;
            if ((from product in _products where product.Id == productId select product).Count() == 0) return 0;
            return (from product in _products where product.Id == productId select product.Quantity).ElementAt(0);
        }
    }
}
