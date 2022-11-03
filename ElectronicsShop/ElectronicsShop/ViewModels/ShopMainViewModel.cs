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
            Products = _productsService.GetProducts();
        }
    }
}
