namespace ElectronicsShop.Views.UserViews;

public partial class OrderDetailsView : ContentPage
{
    public OrderDetailsView(OrderDetailsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}