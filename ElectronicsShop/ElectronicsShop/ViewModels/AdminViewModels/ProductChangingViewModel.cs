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
        private string _oldImage = null;

        public ProductChangingViewModel(ProductsService productsService, CategoryService categoryService)
        {
            _productsService = productsService;
            _categoryService = categoryService;


            GetCategories();

            PropertyChanged += ProductChanged;
            PropertyChanged += CheckEmpty;
            _categoryService.CategoryChanged += CategoryChanged;
        }

        public async void GetCategories()
        {
            IsBusy = true;
            try
            {
                Categories = (from category in await _categoryService.GetCategories() select category.CategoryName).ToObservableCollection();
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
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

            _oldImage ??= Product.ImageURI;
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
            try
            {
                Product.ProductName = ProductName;
                Product.ProductCategory = ProductCategory;
                Product.Manufacturer = Manufacturer;
                Product.Price = Double.Parse(Price);
                Product.Description = Description;
                Product.ImageURI = ImageURI;

                await _productsService.ChangeProductAsync(Product, _image, _oldImage);
                await Shell.Current.DisplayAlert("Success", "Product changed", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
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
