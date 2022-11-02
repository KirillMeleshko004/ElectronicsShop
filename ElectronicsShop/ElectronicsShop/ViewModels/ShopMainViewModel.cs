using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class ShopMainViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public ShopMainViewModel()
        {
            Products = new()
            {
                new Product("Smartphone", 1, "lol_1", "lol_1_manufacturer", 10000, "lol_1_description", 10),
                new Product("Smartphone", 1, "lol_2", "lol_2_manufacturer", 10000, "lol_2_description", 10),
                new Product("Smartphone", 1, "lol_3", "lol_3_manufacturer", 10000, "lol_3_description", 10)
            };
        }
    }
}
