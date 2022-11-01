namespace ElectronicsShop.Views;

public partial class ShopMainView : ContentPage
{
	public ShopMainView(ShopMainViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}