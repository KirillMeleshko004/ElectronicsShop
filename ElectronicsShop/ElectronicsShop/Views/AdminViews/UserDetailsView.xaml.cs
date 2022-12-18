namespace ElectronicsShop.Views.AdminViews;

public partial class UserDetailsView : ContentPage
{
	public UserDetailsView(UserDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}