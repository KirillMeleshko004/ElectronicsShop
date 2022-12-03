namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class ProductCreationViewModel : BaseViewModel
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


        private FileResult _image;
        private readonly ProductsService _productsService;
        public ProductCreationViewModel(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [RelayCommand]
        public async Task AddProduct()
        {

            Product productToAdd = new Product
            {
                ProductName = productName,
                ProductCategory = productCategory,
                Manufacturer = manufacturer,
                Price = price,
                Description = description
            };
            await _productsService.AddProductAsync(productToAdd, _image);
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
