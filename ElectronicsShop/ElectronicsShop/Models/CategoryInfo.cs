using ElectronicsShop.GlobalConst;
using static ElectronicsShop.GlobalConst.CategoriesConst;

namespace ElectronicsShop.Models
{
    public class CategoryInfo
    {
        public string CategoryName { get; }
        public ImageSource Icon { get; }
        private CategoryInfo(string category, string image)
        {
            CategoryName = category;
            Icon = ImageSource.FromFile(image);
        }
        public static List<CategoryInfo> CategoriesList { get; } = new()
        {
            new CategoryInfo(SMARTPHONES_CATEGORY, Icons.SmartphoneCategoryIcon),
            new CategoryInfo(TV_CATEGORY, Icons.TvCategoryIcon),
            new CategoryInfo(KITCHEN_CATEGORY, Icons.KitchenAppliancesCategoryIcon),
            new CategoryInfo(HOME_CATEGORY, Icons.HomeAppliancesCategoryIcon),
            new CategoryInfo(AUDIO_CATEGORY, Icons.AudioCategoryIcon),
            new CategoryInfo(LAPTOPS_CATEGORY, Icons.LaptopCategoryIcon)
        };
    }
}
