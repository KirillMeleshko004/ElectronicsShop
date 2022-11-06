using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class CategoriesViewModel : BaseViewModel
    {
        public List<CategoryInfo> CategoriesList { get; set; }
        public CategoriesViewModel()
        {
            CategoriesList = CategoryInfo.CategoriesList;
        }
    }
}
