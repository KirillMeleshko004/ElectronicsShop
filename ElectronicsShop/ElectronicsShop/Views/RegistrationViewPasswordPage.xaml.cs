namespace ElectronicsShop.Views;

public partial class RegistrationViewPasswordPage : ContentPage
{
	public RegistrationViewPasswordPage(RegistrationViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}