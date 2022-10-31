using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views;

public partial class AuthorizationView : ContentPage
{
	public AuthorizationView(AuthorizationViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}