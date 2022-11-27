namespace ElectronicsShop.Views;

public partial class OrdersView : ContentPage
{
	public OrdersView(OrdersViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}