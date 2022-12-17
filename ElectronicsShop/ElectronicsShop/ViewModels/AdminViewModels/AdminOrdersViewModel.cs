using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class AdminOrdersViewModel : BaseViewModel, IRefreshableAsync
    {
        [ObservableProperty]
        ObservableCollection<Order> _orders;

        OrderService _orderService;
        public AdminOrdersViewModel(OrderService orderService)
        {
            _orderService = orderService;

            RefreshAsync();
        }
        public async void RefreshAsync()
        {
            Orders = (await _orderService.GetAllOrdersAsync()).ToObservableCollection();
        }

        [RelayCommand]
        async Task GoToOrder(Order order)
        {
            await Shell.Current.GoToAsync($"{nameof(AdminOrderDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentOrder"] = order
                });
        }
    }
}
