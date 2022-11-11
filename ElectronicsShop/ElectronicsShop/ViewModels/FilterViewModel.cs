using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(FilterProduct), nameof(FilterProduct))]
    public partial class FilterViewModel : BaseViewModel
    {
        FilterService _filterService;

        Product FilterProduct { get; set; }

        [ObservableProperty]
        ObservableCollection<Product> products;

        public FilterViewModel(FilterService filterService)
        {
            _filterService = filterService;
        }

        public void SetFilter()
        {
            Products = new(_filterService.FilterProducts(FilterProduct));
        }
    }
}
