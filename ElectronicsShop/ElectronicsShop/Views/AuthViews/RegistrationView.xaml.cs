namespace ElectronicsShop.Views.AuthViews;

public partial class RegistrationView : ContentPage
{
	public RegistrationView(RegistrationViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}