using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class CategoriesViewModel : BaseViewModel
    {
        ProductsService _productsService;
        public ObservableCollection<Product> Categories { get; set; }
        public CategoriesViewModel(ProductsService productsService)
        {
            _productsService = productsService;
            Categories = _productsService.GetProducts();
        }
    }
}
