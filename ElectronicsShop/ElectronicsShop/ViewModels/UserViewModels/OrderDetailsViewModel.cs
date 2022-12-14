namespace ElectronicsShop.ViewModels.UserViewModels
{
    [QueryProperty(nameof(CurrentOrder), nameof(CurrentOrder))]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCancelable))]
        Order _currentOrder;

        public bool IsCancelable => Order.IsCancelable(CurrentOrder?.Status ?? null);

        private readonly OrderService _orderService;
        public OrderDetailsViewModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        [RelayCommand]
        async Task RequestCancellationAsync()
        {
            IsBusy = true;
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                IsBusy = false;
                return;
            }
            CurrentOrder = await _orderService.ChangeStatusAsync(CurrentOrder.OrderId, OrderStatus.CANCELLATION_REQUESTED);
            IsBusy = false;
        }
    }
}
