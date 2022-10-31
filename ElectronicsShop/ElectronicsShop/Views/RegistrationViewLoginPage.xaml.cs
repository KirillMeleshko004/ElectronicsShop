using AndroidX.Lifecycle;

namespace ElectronicsShop.Views;

public partial class RegistrationViewLoginPage : ContentPage
{
	public RegistrationViewLoginPage(RegistrationLoginViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}