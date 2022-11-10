using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class SearchViewModel : BaseViewModel
    {
        public List<CategoryInfo> CatalogList { get; set; }
        public SearchViewModel()
        {
            CatalogList = CategoryInfo.CatalogList;
        }
    }
}
