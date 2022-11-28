using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(Products), nameof(Products))]
    [QueryProperty(nameof(UserName), nameof(UserName))]
    [QueryProperty(nameof(TotalPrice), nameof(TotalPrice))]
    public partial class CheckoutViewModel : BaseViewModel
    {
        readonly OrderService _orderService;
        readonly CartService _cartService;

        [ObservableProperty]
        ObservableCollection<Product> products;

        [ObservableProperty]
        string userName;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsFailed))]
        bool isSuccessful;
        public bool IsFailed => !isSuccessful;

        [ObservableProperty]
        double totalPrice;

        public CheckoutViewModel(OrderService orderService, CartService cartService)
        {
            _orderService = orderService;

            _cartService = cartService;
        }

        [RelayCommand]
        async Task Confirm()
        {
            int newOrderId = await _orderService.GetOrderId();
            DateTime orderTime = DateTime.Now;
            IsSuccessful = await _orderService.СheckoutAsync(new Order(Products.ToList<Product>(), orderTime, UserName, TotalPrice, newOrderId));
            _cartService.ClearCart();
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
