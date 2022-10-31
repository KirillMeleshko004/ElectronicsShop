using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views;

public partial class AuthorizationView : ContentPage
{
	public AuthorizationView(AuthorizationViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    private async void SignUpGestureRecognizerTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrationViewLoginPage));
    }
}