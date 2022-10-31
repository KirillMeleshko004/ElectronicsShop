using AndroidX.Lifecycle;

namespace ElectronicsShop.Views;

public partial class RegistrationView : ContentPage
{
	public RegistrationView(RegistrationViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}