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
            Product filterProduct = new Product() { ProductType = categoryInfo.CategoryName };
            await Shell.Current.GoToAsync($"{nameof(FilterView)}",
                new Dictionary<string, object>
                {
                    ["FilterProduct"] = filterProduct
                });
        }
    }
}
