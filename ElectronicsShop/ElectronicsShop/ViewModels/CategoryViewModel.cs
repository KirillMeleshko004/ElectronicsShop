using CommunityToolkit.Maui.Core.Extensions;
using ElectronicsShop.Views;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class CategoryViewModel : BaseViewModel
    {
        public List<CategoryInfo> CatalogList { get; set; }

        public static List<Product> Products { get; private set; } = new();
        public CategoryViewModel(ProductsService productsService)
        {
            CatalogList = CategoryInfo.CatalogList;
            Products = productsService.GetProducts();
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
