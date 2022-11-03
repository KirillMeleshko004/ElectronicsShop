using System.Collections.ObjectModel;

namespace ElectronicsShop.Services
{
    public class ProductsService
    {
        public ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> products = new()
            {
                new Product("Fridges", 1, "lol_1", "lol_1_manufacturer", 10000, "lol_1_description", 10, "fridge.jpg"),
                new Product("Toasters", 1, "lol_2", "lol_2_manufacturer", 10000, "lol_2_description", 10, "toaster.jpg"),
                new Product("Tvs", 1, "lol_3", "lol_3_manufacturer", 10000, "lol_3_description", 10, "tv.jpg")
            };
            return products;
        }
    }
}
