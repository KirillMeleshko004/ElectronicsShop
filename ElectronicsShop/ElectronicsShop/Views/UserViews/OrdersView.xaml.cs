namespace ElectronicsShop.Views.UserViews;

public partial class OrdersView : ContentPage
{
	public OrdersView(OrdersViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}