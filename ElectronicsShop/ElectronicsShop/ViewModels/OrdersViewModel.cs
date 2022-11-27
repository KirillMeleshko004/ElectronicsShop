using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(UserName), nameof(UserName))]
    public partial class OrdersViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Order> orders;

        [ObservableProperty]
        string userName;
    }
}
