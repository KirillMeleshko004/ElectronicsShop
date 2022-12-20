using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class ProductCreationViewModel : BaseViewModel
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
        string imageUrl;


        [ObservableProperty]
        bool isNotEmpty = false;


        private FileResult _image;
        private readonly ProductsService _productsService;
        readonly CategoryService _categoryService;
        public ProductCreationViewModel(ProductsService productsService, CategoryService categoryService)
        {
            _productsService = productsService;
            _categoryService = categoryService;

            GetCategories();

            PropertyChanged += CheckEmpty;
            _categoryService.CategoryChanged += CategoryChanged;
        }

        public async void GetCategories()
        {
            Categories = (from category in await _categoryService.GetCategories() select category.CategoryName)?.ToObservableCollection();
        }

        private void CheckEmpty(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ProductName)
                && e.PropertyName != nameof(ProductCategory)
                && e.PropertyName != nameof(Manufacturer)
                && e.PropertyName != nameof(Description)
                && e.PropertyName != nameof(Price)
                && e.PropertyName != nameof(ImageUrl)) return;
            if (string.IsNullOrEmpty(ProductName) ||
                string.IsNullOrEmpty(ProductCategory) ||
                string.IsNullOrEmpty(Manufacturer) ||
                string.IsNullOrEmpty(Description) ||
                string.IsNullOrEmpty(Price) ||
                string.IsNullOrEmpty(ImageUrl))
                IsNotEmpty = false;
            else IsNotEmpty = true;
        }
        private void CategoryChanged(object sender, EventArgs e)
        {
            GetCategories();
        }

        [RelayCommand]
        public async Task AddProduct()
        {
            IsBusy = true;
            Product productToAdd = new()
            {
                ProductName = productName,
                ProductCategory = productCategory,
                Manufacturer = manufacturer,
                Price = Double.Parse(price),
                Description = description
            };
            await _productsService.AddProductAsync(productToAdd, _image);
            await Shell.Current.DisplayAlert("Success", "Product added", "Ok");
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
            string action = await Shell.Current.DisplayActionSheet("What to do?", "Cancel", null, "Create", "Change", "Delete");
            switch(action)
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
