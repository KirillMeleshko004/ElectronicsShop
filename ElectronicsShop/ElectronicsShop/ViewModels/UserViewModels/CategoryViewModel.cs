using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class CategoryViewModel : BaseViewModel
    {
        readonly ProductsService _productsService;
        public List<CategoryInfo> CatalogList { get; set; }
        [ObservableProperty]
        ObservableCollection<Product> products;
        public CategoryViewModel(ProductsService productsService)
        {
            CatalogList = CategoryInfo.CatalogList;
            _productsService = productsService;

            Refresh();
        }

        [RelayCommand]
        async Task GoToCategory(CategoryInfo categoryInfo)
        {
            ObservableCollection<Product> categoryProducts = (from product in Products
                                                              where product.ProductCategory == categoryInfo.CategoryName
                                                              select product).ToObservableCollection<Product>();
            await Shell.Current.GoToAsync($"{nameof(ProductsListView)}",
                new Dictionary<string, object>
                {
                    ["Title"] = categoryInfo.CategoryName,
                    ["Products"] = categoryProducts
                });
        }
        public async void Refresh()
        {
            CatalogList = CategoryInfo.CatalogList;

            Products = (await _productsService.GetProductsAsync()).ToObservableCollection();
        }
    }
}
