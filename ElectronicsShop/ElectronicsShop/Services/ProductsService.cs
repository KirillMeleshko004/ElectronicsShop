using System.Collections.ObjectModel;
using static ElectronicsShop.GlobalConst.SearchConst;

namespace ElectronicsShop.Services
{
    public class ProductsService
    {
        public ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> products = new()
            {
                new Product(KITCHEN_CATEGORY, 1, "Fridge_1", "lol_1_manufacturer", 10000, "lol_1_description", 10, "fridge.jpg"),
                new Product(KITCHEN_CATEGORY, 1, "Toaster_1", "lol_2_manufacturer", 10000, "lol_2_description", 10, "toaster.jpg"),
                new Product(TV_CATEGORY, 1, "TV_1", "lol_3_manufacturer", 10000, "lol_3_description", 10, "tv.jpg"),
                new Product(TV_CATEGORY, 1, "TV_2", "lol_3_manufacturer", 10000, "lol_3_description", 10, "tv2.jpg"),
                new Product(SMARTPHONES_CATEGORY, 1, "Smartphone_1", "lol_3_manufacturer", 10000, "lol_3_description", 10, "smartphone.jpg"),
                new Product(HOME_CATEGORY, 1, "Homeapl_1", "lol_3_manufacturer", 10000, "lol_3_description", 10, "washing_machine.jpg"),
                new Product(LAPTOPS_CATEGORY, 1, "Laptop_1", "lol_3_manufacturer", 10000, "lol_3_description", 10, "laptop.jpg")
            };
            return products;
        }
    }
}
