using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    [QueryProperty(nameof(Product), nameof(Product))]
    public partial class ProductChangingViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<string> _categories;

        [ObservableProperty]
        string productName;
        [ObservableProperty]
        string productCategory;
        [ObservableProperty]
        string manufacturer;
        [ObservableProperty]
        double price;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string imageUrl;

        [ObservableProperty]
        Product _product;

        private FileResult _image = null;
        private readonly ProductsService _productsService;
        private readonly CategoryService _categoryService;

        public ProductChangingViewModel(ProductsService productsService, CategoryService categoryService)
        {
            _productsService = productsService;
            _categoryService = categoryService;


            GetCategories();

            PropertyChanged += ProductChanged;
        }

        public async void GetCategories()
        {
            Categories = (from category in await _categoryService.GetCategories() select category.CategoryName).ToObservableCollection();
        }
        private void CategoryChanged(object sender, EventArgs e)
        {
            GetCategories();
        }

        void ProductChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Product)) return;
            ProductName = Product.ProductName;
            ProductCategory = Product.ProductCategory;
            Manufacturer = Product.Manufacturer;
            Price = Product.Price;
            Description = Product.Description;
            ImageUrl = Product.ImageString;
        }

        [RelayCommand]
        public async Task ChangeProduct()
        {
            IsBusy = true;
            Product.ProductName = ProductName;
            Product.ProductCategory = ProductCategory;
            Product.Manufacturer = Manufacturer;
            Product.Price = Price;
            Product.Description = Description;
            Product.ImageString = ImageUrl;

            await _productsService.ChangeProductAsync(Product, _image);
            await Shell.Current.DisplayAlert("Success", "Product changed", "Ok");
            await Shell.Current.GoToAsync("..");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task PickImage()
        {
            _image = await MediaPicker.PickPhotoAsync();
            if (_image is null) return;
            ImageUrl = _image.FullPath;
        }


        [RelayCommand]
        public async Task CreateCategory()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(CategoryCreationView)}");
            IsBusy = false;
        }
    }
}
