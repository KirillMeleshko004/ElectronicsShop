using System.Collections.ObjectModel;

namespace ElectronicsShop.SearchHandlers
{
    public class OrderSearchHandler : SearchHandler
    {
        public static readonly BindableProperty OrdersProperty = BindableProperty.Create(
            nameof(Orders),
            typeof(ObservableCollection<Order>),
            typeof(ProductSearchHandler),
            propertyChanged: ProductsChanged);
        public ObservableCollection<Order> Orders
        {
            get => (ObservableCollection<Order>)GetValue(OrdersProperty);
            set => SetValue(OrdersProperty, value);
        }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            if (Orders is null) return;
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrEmpty(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = (from order in Orders
                               where order.OrderId.ToString().ToLower().Contains(newValue.ToLower())
                               select order).ToList();
            }
        }
        protected override async void OnItemSelected(object item)
        {
            Order currentOrder = (Order)item;

            await Shell.Current.GoToAsync($"{nameof(AdminOrderDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentOrder"] = currentOrder
                });
        }

        static void ProductsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OrderSearchHandler searchHandler = (OrderSearchHandler)bindable;
            searchHandler.Orders = (ObservableCollection<Order>)newValue;
        }

    }
}