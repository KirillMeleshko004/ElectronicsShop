namespace ElectronicsShop.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView(CheckoutViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}