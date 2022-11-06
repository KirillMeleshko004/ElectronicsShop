using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicsShop.GlobalConst;

namespace ElectronicsShop.Models
{
    public class Categories
    {
        public string Category { get; set; }
        public ImageSource Icon { get; set; }
        private Categories(string category, string image)
        {
            Category = category;
            Icon = ImageSource.FromFile(image);
        }
        public static List<Categories> CategoriesList { get; } = new()
        {
            new Categories("Smartphones", "smartphone_icon.png"),
            new Categories("TVs", Icons.TvCategoryIcon),
            new Categories("Kitchen Appliances", Icons.KitchenAppliancesCategoryIcon),
            new Categories("Home Appliances", Icons.HomeAppliancesCategoryIcon),
            new Categories("Audio", Icons.AudioCategoryIcon),
            new Categories("Laptops", Icons.LaptopCategoryIcon)
        };
    }
}
