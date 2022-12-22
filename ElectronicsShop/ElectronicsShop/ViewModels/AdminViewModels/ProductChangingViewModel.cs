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
        string price;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string _imageURI;

        [ObservableProperty]
        bool isNotEmpty = false;

        [ObservableProperty]
        Product _product;

        private FileResult _image = null;
        private readonly ProductsService _productsService;
        private readonly CategoryService _categoryService;
        private Product _oldProduct = null;

        public ProductChangingViewModel(ProductsService productsService, CategoryService categoryService)
        {
            try
            {

                _productsService = productsService;
                _categoryService = categoryService;


                GetCategories();

                PropertyChanged += ProductChanged;
                PropertyChanged += CheckEmpty;
                _categoryService.CategoryChanged += CategoryChanged;
            }
            catch(Exception e)
            {
                string m = e.Message;
                Console.WriteLine(m);
            }
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
            Price = Product.Price.ToString();
            Description = Product.Description;
            ImageURI = Product.ImageURI;
            
            _oldProduct ??= Product.CloneProduct();
        }
        private void CheckEmpty(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ProductName)
                && e.PropertyName != nameof(ProductCategory)
                && e.PropertyName != nameof(Manufacturer)
                && e.PropertyName != nameof(Description)
                && e.PropertyName != nameof(Price)) return;
            if (string.IsNullOrEmpty(ProductName) ||
                string.IsNullOrEmpty(ProductCategory) ||
                string.IsNullOrEmpty(Manufacturer) ||
                string.IsNullOrEmpty(Description) ||
                string.IsNullOrEmpty(Price))
                IsNotEmpty = false;
            else IsNotEmpty = true;
        }

        [RelayCommand]
        public async Task ChangeProduct()
        {
            IsBusy = true;
            Product.ProductName = ProductName;
            Product.ProductCategory = ProductCategory;
            Product.Manufacturer = Manufacturer;
            Product.Price = Double.Parse(Price);
            Product.Description = Description;
            Product.ImageURI = ImageURI;

            await _productsService.ChangeProductAsync(Product, _image, _oldProduct);
            await Shell.Current.DisplayAlert("Success", "Product changed", "Ok");
            await Shell.Current.GoToAsync("..");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task PickImage()
        {
            _image = await MediaPicker.PickPhotoAsync();
            if (_image is null) return;
            ImageURI = _image.FullPath;
        }



        [RelayCommand]
        public async Task CategoryAction()
        {
            IsBusy = true;
            string action = await Shell.Current.DisplayActionSheet("What to do?", "Cancel", null, "Create", "Change", "Delete");
            switch (action)
            {
                case "Create":
                    await Shell.Current.GoToAsync($"{nameof(CategoryCreationView)}");
                    break;

                case "Change":
                    await Shell.Current.GoToAsync($"{nameof(CategoryChangingView)}");
                    break;

                case "Delete":
                    await Shell.Current.GoToAsync($"{nameof(DeleteCategoryView)}");
                    break;
            }
            IsBusy = false;
        }
    }
}
