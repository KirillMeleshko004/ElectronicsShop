namespace ElectronicsShop.Views;

public partial class CartView : ContentPage
{
	public CartView(CartViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}