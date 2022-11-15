using ElectronicsShop.Views;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(FilterProduct), nameof(FilterProduct))]
    public partial class ProductsListViewModel : BaseViewModel
    {
        ProductsService _productsService;

        Product _filterProduct;
        public Product FilterProduct 
        { 
            get
            {
                return _filterProduct;
            } 
            set
            {
                _filterProduct = value;
                DataTransferred(this, null);
            }
        }

        [ObservableProperty]
        ObservableCollection<Product> _products;
        public ProductsListViewModel(ProductsService productsService)
        {
            _productsService = productsService;

            DataTransferred += DataTransferredAction;
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

        event EventHandler DataTransferred;

        public void DataTransferredAction(object sender, EventArgs e)
        {
            Products = new(_productsService.GetProducts(_filterProduct));
        }
    }
}
