namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(CurrentOrder), nameof(CurrentOrder))]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Order currentOrder;
    }
}
