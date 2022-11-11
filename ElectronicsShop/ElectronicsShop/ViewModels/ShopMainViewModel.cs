using ElectronicsShop.Views;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class ShopMainViewModel : BaseViewModel
    {
        ProductsService _productsService;
        public ObservableCollection<Product> Products { get; set; }
        public ShopMainViewModel(ProductsService productsService)
        {
            _productsService = productsService;
            Products = new(_productsService.GetProducts());
        }
        [RelayCommand]
        async Task GoToProduct(Product currentProduct)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct
                });
        }
    }
}
