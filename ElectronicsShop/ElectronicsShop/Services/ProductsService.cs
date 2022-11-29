using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using static ElectronicsShop.GlobalConst.SearchConst;

namespace ElectronicsShop.Services
{
    public class ProductsService
    {
        public List<Product> GetProducts()
        {
            return TempServer.GetProducts();
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await TempServer.GetProductsAsync();
        }
    }
}
