using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class SearchViewModel : BaseViewModel
    {
        public List<CategoryInfo> SearchList { get; set; }
        public SearchViewModel()
        {
            SearchList = CategoryInfo.SearchList;
        }
    }
}
