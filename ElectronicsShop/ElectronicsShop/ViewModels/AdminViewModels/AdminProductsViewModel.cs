using CommunityToolkit.Maui.Core.Extensions;
using ElectronicsShop.Models;
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
            _productsService.ProductChanged += ProductChanged;

            GetProducts();
        }

        async void GetProducts()
        {
            Products = (await _productsService.GetProductsAsync()).ToObservableCollection<Product>();
            if (Products.Count != 0) IsEmpty = false;
        }

        [RelayCommand]
        async Task AddProduct()
        {
            await Shell.Current.GoToAsync($"{nameof(ProductCreationView)}");
        }

        [RelayCommand]
        async Task DeleteProduct(Product product)
        {
            await _productsService.DeleteProductAsync(product);
        }

        [RelayCommand]
        async Task ChangeProduct(Product product)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductChangingView)}",
                new Dictionary<string, object>
                {
                    ["Product"] = product
                });
        }

        void ProductChanged(object sender, ProductEventArgs e)
        {
            switch(e.Action)
            {
                case ProductEventArgs.Actions.added:
                    Products.Add(e.Product);
                    if (IsEmpty) IsEmpty = false;
                    break;
                case ProductEventArgs.Actions.removed:
                    Products.Remove(e.Product);
                    if (IsNotEmpty) IsEmpty = true;
                    break;
                case ProductEventArgs.Actions.changed:
                    Products[Products.IndexOf(e.Product)] = e.Product;
                    break;
            }
        }
    }
}
