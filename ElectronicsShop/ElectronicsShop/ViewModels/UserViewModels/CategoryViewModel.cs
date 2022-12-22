using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class CategoryViewModel : BaseViewModel, IRefreshableAsync
    {
        readonly ProductsService _productsService;
        readonly CategoryService _categoryService;

        [ObservableProperty]
        ObservableCollection<Category> _categories;
        [ObservableProperty]
        ObservableCollection<Product> products;
        public CategoryViewModel(ProductsService productsService, CategoryService categoryService)
        {
            _productsService = productsService;
            _categoryService = categoryService;

            RefreshAsync();
        }

        [RelayCommand]
        async Task GoToCategory(Category category)
        {
            ObservableCollection<Product> categoryProducts = (from product in Products
                                                              where product.ProductCategory == category.CategoryName
                                                              select product).ToObservableCollection<Product>();
            await Shell.Current.GoToAsync($"{nameof(ProductsListView)}",
                new Dictionary<string, object>
                {
                    ["Title"] = category.CategoryName,
                    ["Products"] = categoryProducts
                });
        }
        public async void RefreshAsync()
        {
            IsBusy = true;
            try
            {
                Categories = (await _categoryService.GetCategories()).ToObservableCollection();

                Products = (await _productsService.GetProductsAsync()).ToObservableCollection();
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
