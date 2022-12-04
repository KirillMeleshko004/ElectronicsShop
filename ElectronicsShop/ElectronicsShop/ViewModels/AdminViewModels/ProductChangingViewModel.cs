using ElectronicsShop.Models;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    [QueryProperty(nameof(Product), nameof(Product))]
    public partial class ProductChangingViewModel : BaseViewModel
    {
        public static readonly List<string> Categories = new List<string>()
        {
            CategoriesConst.KITCHEN_CATEGORY,
            CategoriesConst.SMARTPHONES_CATEGORY,
            CategoriesConst.AUDIO_CATEGORY,
            CategoriesConst.LAPTOPS_CATEGORY,
            CategoriesConst.TV_CATEGORY,
            CategoriesConst.HOME_CATEGORY,
        };

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

        public ProductChangingViewModel(ProductsService productsService)
        {
            _productsService = productsService;

            PropertyChanged += ProductChanged;
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
            Product.ProductName = ProductName;
            Product.ProductCategory = ProductCategory ;
            Product.Manufacturer = Manufacturer ;
            Product.Price = Price;
            Product.Description = Description;
            Product.ImageString = ImageUrl;

            await _productsService.ChangeProductAsync(Product, _image);
        }

        [RelayCommand]
        public async Task PickImage()
        {
            _image = await MediaPicker.PickPhotoAsync();
            if (_image is null) return;
            ImageUrl = _image.FullPath;
        }
    }
}
