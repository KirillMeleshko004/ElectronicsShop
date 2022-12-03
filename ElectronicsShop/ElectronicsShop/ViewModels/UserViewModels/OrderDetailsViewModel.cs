namespace ElectronicsShop.ViewModels.UserViewModels
{
    [QueryProperty(nameof(CurrentOrder), nameof(CurrentOrder))]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Order currentOrder;
    }
}
