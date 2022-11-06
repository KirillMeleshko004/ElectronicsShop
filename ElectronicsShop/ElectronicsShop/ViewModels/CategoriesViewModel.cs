using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class CategoriesViewModel : BaseViewModel
    {
        public List<Categories> CategoriesList { get; set; }
        public CategoriesViewModel()
        {
            CategoriesList = Categories.CategoriesList;
        }
    }
}
