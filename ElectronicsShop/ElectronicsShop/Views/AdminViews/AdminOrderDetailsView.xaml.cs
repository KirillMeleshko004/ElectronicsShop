namespace ElectronicsShop.Views.AdminViews;

public partial class AdminOrderDetailsView : ContentPage
{
	public AdminOrderDetailsView(AdminOrderDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}