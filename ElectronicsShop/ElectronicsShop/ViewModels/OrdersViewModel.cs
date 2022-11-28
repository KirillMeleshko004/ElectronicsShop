using ElectronicsShop.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels
{
    [QueryProperty(nameof(UserName), nameof(UserName))]
    public partial class OrdersViewModel : BaseViewModel
    {
        readonly OrderService _orderService;

        [ObservableProperty]
        ObservableCollection<Order> orders;

        [ObservableProperty]
        string userName;
        [ObservableProperty]
        int orderId;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;

        public OrdersViewModel(OrderService orderService)
        {
            _orderService = orderService;

            PropertyChanged += UserChanged;
            PropertyChanged += OrdersChanged;
        }

        async void UserChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(UserName)) return;

            Title = $"{UserName}'s orders";
            Orders = new ObservableCollection<Order>(await _orderService.GetOrdersAsync(UserName));
        }
        void OrdersChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Orders)) return;
            if (Orders.Count == 0) IsEmpty = true;
            else IsEmpty = false;
        }

        [RelayCommand]
        async Task GoToOrder(Order order)
        {
            await Shell.Current.GoToAsync($"{nameof(OrderDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentOrder"] = order
                });
        }
    }
}
