using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class AdminProductsViewModel : BaseViewModel
    {
        readonly ProductsService _productsService;

        [ObservableProperty]
        ObservableCollection<Product> _products;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;
        public AdminProductsViewModel(ProductsService productsService)
        {
            _productsService = productsService;
            GetProducts();
            PropertyChanged += CollectionChanged;
        }

        async void GetProducts()
        {
            Products = (await _productsService.GetProductsAsync()).ToObservableCollection<Product>();
        }

        [RelayCommand]
        async Task GoToProduct(Product currentProduct)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct
                });
        }
        [RelayCommand]
        async Task AddProduct()
        {
            await Shell.Current.GoToAsync($"{nameof(ProductCreationView)}");
        }

        public void CollectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Products)) return;
            if (Products.Count == 0) IsEmpty = true;
            else IsEmpty = false;
        }
    }
}
