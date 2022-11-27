using ElectronicsShop.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(Products), nameof(Products))]
    public partial class ProductsListViewModel : BaseViewModel
    {
        ProductsService _productsService;

        [ObservableProperty]
        ObservableCollection<Product> _products;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;
        public ProductsListViewModel(ProductsService productsService)
        {
            _productsService = productsService;

            PropertyChanged += CollectionChanged;
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

        public void CollectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Products)) return;
            if (Products.Count == 0) IsEmpty = true;
            else IsEmpty = false;
        }
    }
}
