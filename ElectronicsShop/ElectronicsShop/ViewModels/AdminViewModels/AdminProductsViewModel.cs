using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class AdminProductsViewModel : BaseViewModel, IRefreshableAsync
    {
        readonly ProductsService _productsService;
        readonly CategoryService _categoryService;

        [ObservableProperty]
        ObservableCollection<Product> _products;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;
        public AdminProductsViewModel(ProductsService productsService, CategoryService categoryService)
        {
            _productsService = productsService;
            _categoryService = categoryService;
            _productsService.ProductChanged += ProductChanged;

            RefreshAsync();
        }

        public async void RefreshAsync()
        {
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                return;
            }

            Products = (from product in await _productsService.GetProductsAsync()
                        orderby product.ProductName
                        select product).ToObservableCollection<Product>();
            if (Products.Count != 0) IsEmpty = false;
        }

        [RelayCommand]
        async Task AddProduct()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ProductCreationView)}");
            IsBusy = false;
        }

        [RelayCommand]
        async Task DeleteProduct(Product product)
        {
            IsBusy = true;

            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                IsBusy = false;
                return;
            }

            bool choice = await Shell.Current.DisplayAlert("Are you sure?!",
                    $"Product {product.ProductName} will be deleted",
                    "Confirm",
                    "Cancel");
            if (choice) await _productsService.DeleteProductAsync(product);
            IsBusy = false;
        }

        [RelayCommand]
        async Task ChangeProduct(Product product)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ProductChangingView)}",
                new Dictionary<string, object>
                {
                    ["Product"] = product
                });
            IsBusy = false;
        }

        void ProductChanged(object sender, ProductEventArgs e)
        {
            switch (e.Action)
            {
                case ProductEventArgs.Actions.added:
                    Products.Add(e.Product);
                    if (IsEmpty) IsEmpty = false;
                    break;
                case ProductEventArgs.Actions.removed:
                    Products.Remove(e.Product);
                    if (Products.Count == 0) IsEmpty = true;
                    break;
                case ProductEventArgs.Actions.changed:
                    Products[Products.IndexOf(e.Product)] = e.Product;
                    break;
            }
        }
    }
}
