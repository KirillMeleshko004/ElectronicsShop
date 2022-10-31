namespace ElectronicsShop.Views;

public partial class RegistrationViewPasswordPage : ContentPage
{
	public RegistrationViewPasswordPage(RegistrationLoginViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}