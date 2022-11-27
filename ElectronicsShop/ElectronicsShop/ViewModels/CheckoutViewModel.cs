using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(Products), nameof(Products))]
    [QueryProperty(nameof(UserName), nameof(UserName))]
    [QueryProperty(nameof(TotalPrice), nameof(TotalPrice))]
    public partial class CheckoutViewModel : BaseViewModel
    {
        readonly OrderService _orderService;

        [ObservableProperty]
        ObservableCollection<Product> products;

        [ObservableProperty]
        string userName;

        [ObservableProperty]
        double totalPrice;

        public CheckoutViewModel(OrderService orderService)
        {
            _orderService = orderService;
        }
    }
}
