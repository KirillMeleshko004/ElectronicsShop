using CommunityToolkit.Maui.Core.Extensions;
using ElectronicsShop.Views;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class CategoryViewModel : BaseViewModel
    {
        public List<CategoryInfo> CatalogList { get; set; }
        public CategoryViewModel()
        {
            CatalogList = CategoryInfo.CatalogList;
        }

        [RelayCommand]
        async Task GoToCategory(CategoryInfo categoryInfo)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductsListView)}",
                new Dictionary<string, object>
                {
                    ["FilterProduct"] = new Product() { ProductType = categoryInfo.CategoryName }
                });
        }
    }
}
