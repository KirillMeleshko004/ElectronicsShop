namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(CurrentProduct), nameof(CurrentProduct))]
    public partial class ProductViewModel : BaseViewModel
    {
        [ObservableProperty]
        Product currentProduct;
    }
}
