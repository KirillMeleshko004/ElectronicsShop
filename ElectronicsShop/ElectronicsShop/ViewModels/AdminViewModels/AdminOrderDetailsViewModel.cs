using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    [QueryProperty(nameof(CurrentOrder), nameof(CurrentOrder))]
    public partial class AdminOrderDetailsViewModel : BaseViewModel
    {
        public static readonly List<string> orderStatuses = OrderStatus.STATUSES;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCancelable))]
        Order _currentOrder;

        [ObservableProperty]
        bool _isChanged = false;

        [ObservableProperty]
        string _status;

        public bool IsCancelable => Order.IsCancelable(CurrentOrder?.Status ?? null);

        private readonly OrderService _orderService;
        string _oldStatus;
        public AdminOrderDetailsViewModel(OrderService orderService)
        {
            _orderService = orderService;

            PropertyChanged += OrderChanged;
            PropertyChanged += StatusChanged;
        }

        public void OrderChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CurrentOrder)) return;
            _oldStatus = CurrentOrder.Status;
            Status = CurrentOrder.Status;
        }
        public void StatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Status)) return;
            if (Status == _oldStatus) IsChanged = false;
            else IsChanged = true;
        }

        [RelayCommand]
        async Task ChangeStatusAsync()
        {
            IsBusy = true;
            try
            {
                await _orderService.ChangeStatusAsync(CurrentOrder.OrderId, Status);
                _oldStatus = Status;
                string text = "Status changed";
                await Toast.Make(text, ToastDuration.Short).Show();
                IsChanged = false;
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
