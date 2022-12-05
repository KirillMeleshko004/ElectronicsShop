namespace ElectronicsShop.Views.AuthViews;

public partial class AuthorizationView : ContentPage
{
    public AuthorizationView(AuthorizationViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}