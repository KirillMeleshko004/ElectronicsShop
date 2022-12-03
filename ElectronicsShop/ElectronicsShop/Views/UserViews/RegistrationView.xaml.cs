namespace ElectronicsShop.Views.UserViews;

public partial class RegistrationView : ContentPage
{
	public RegistrationView(RegistrationViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}