namespace ElectronicsShop.Views.UserViews;

public partial class OrdersView : ContentPage
{
    OrdersViewModel _viewModel;
    public OrdersView(OrdersViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _viewModel.Refresh();
    }
}