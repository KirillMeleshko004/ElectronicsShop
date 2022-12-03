namespace ElectronicsShop.Views.AdminViews;

public partial class AdminProductsView : ContentPage
{
	public AdminProductsView(AdminProductsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}