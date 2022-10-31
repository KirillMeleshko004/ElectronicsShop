using AndroidX.Lifecycle;

namespace ElectronicsShop.Views;

public partial class RegistrationViewLoginPage : ContentPage
{
	public RegistrationViewLoginPage(RegistrationViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
    private async void LoginConfirmationPropertyChanged(object sender, EventArgs e)
    {
        if (((Label)sender).Text == "false") return;
        await Shell.Current.GoToAsync(nameof(RegistrationViewLoginPage));
    }
}