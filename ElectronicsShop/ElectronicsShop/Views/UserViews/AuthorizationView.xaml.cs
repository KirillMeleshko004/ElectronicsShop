namespace ElectronicsShop.Views.UserViews;

public partial class AuthorizationView : ContentPage
{
	public AuthorizationView(AuthorizationViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}