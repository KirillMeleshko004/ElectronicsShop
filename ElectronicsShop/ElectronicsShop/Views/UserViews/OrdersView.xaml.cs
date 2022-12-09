using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.UserViews;

public partial class OrdersView : ContentPage
{
    IRefreshableAsync _viewModel;
    public OrdersView(OrdersViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _viewModel.RefreshAsync();
    }
}